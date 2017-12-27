using LancooDemo.Common;
using LancooDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LancooDemo.BLL
{
    public class OperationBLL
    {
        public string operationDistance()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();

            OperationDAL opt = new OperationDAL(sh);
            opt.DeleteRecommend();  //删除推荐结果表中数据

            //1、取出所有老师的ID
            List<string> techIDs = opt.getAllTechID();

            //2、构造每个老师的评分表
            Dictionary<string, Dictionary<string, double>> dic = new Dictionary<string, Dictionary<string, double>>();
            dic = opt.makeMarkTable(techIDs);

            //3、计算距离（数据稀疏-->余弦相似度；分数膨胀-->皮尔逊；稀疏且膨胀-->修正余弦相似度）
            //(1).计算每个老师对资源打分的平均值   这部分存在问题
            //Dictionary<string, double> dicAveg = new Dictionary<string, double>();
            //foreach (string tID in dic.Keys)
            //{
            //    double sum = 0.0;
            //    //遍历教师tID对资源的评分
            //    foreach (string res in dic[tID].Keys)
            //    {
            //        sum = sum + dic[tID][res];
            //    }
            //    double aveg = sum / dic[tID].Count;

            //    dicAveg.Add(tID, aveg);
            //}


            //计算每个用户同其他用户的（曼哈顿）距离，并按远近排序
            Dictionary<string, Dictionary<string, double>> sortDic = new Dictionary<string, Dictionary<string, double>>();
            foreach (string tid in dic.Keys)
            {
                Dictionary<string, double>  neghborDic = nearestNeighbor(tid, dic);
                sortDic.Add(tid, neghborDic);
            }

            //4.在排序后的评分字典中为每个取出最相近的K个，把这些用户评分高并且推荐目标没有用过的资源作为推荐项，把推荐结果保存（更新）到数据库
            foreach (string tid in sortDic.Keys)
            {
                Dictionary<string, double> dicJuLi = sortDic[tid];
                List<string> techList = new List<string>();
                int K = 5;  //K近邻
                List<string> recomlist = new List<string>();
                for (int i=0; i<K; i++)
                {
                    KeyValuePair<string, double> kvp = dicJuLi.ElementAt(i);
                    foreach (string res in dic[kvp.Key].Keys)
                    {
                        if (dic[kvp.Key][res] > 3 && !recomlist.Contains(res))
                        {   //评分大于3的加入推荐候选项列表
                            recomlist.Add(res);
                        }
                    }
                }

                //找出本用户已经用过的资源列表
                List<string> usedlist = new List<string>();
                foreach (string res in dic[tid].Keys)
                {
                    usedlist.Add(res);
                }

                //recomlist和usedlist做差集
                recomlist = recomlist.Except(usedlist).ToList<string>();

                //把用户ID和推荐结果集作为参数调用插入数据库的方法
                int temp = opt.saveResult(tid,recomlist);
            }

            string fanhui = "ceshi";
            return fanhui;
        }

        /// <summary>
        /// 计算两个用户的曼哈顿距离
        /// </summary>
        /// <param name="dic1"></param>
        /// <param name="dic2"></param>
        /// <returns></returns>
        public double manhattan(Dictionary<string, double> dic1, Dictionary<string, double> dic2)
        {
            double distance = 0.0;
            foreach (string res in dic1.Keys)
            {
                if (dic2.ContainsKey(res))
                {
                    distance += Math.Abs(dic1[res] - dic2[res]);
                }
            }
            return distance;
        }


        /// <summary>
        /// 找出某个用户同其他用户的距离并排序
        /// </summary>
        /// <param name="techID"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public Dictionary<string, double> nearestNeighbor(string techID, Dictionary<string, Dictionary<string, double>> dic)
        {
            Dictionary<string, double> disDic = new Dictionary<string, double>();

            foreach (string tid in dic.Keys)
            {
                if (techID != tid)
                {
                    double distance = manhattan(dic[techID],dic[tid]);
                    disDic.Add(tid, distance);
                }
               
            }

            disDic = (from entry in disDic
                      orderby entry.Value ascending
                      select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
           
            return disDic;
        }


    }
}