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


            //计算每个用户同其他用户的距离，并按远近排序
            Dictionary<string, Dictionary<string, double>> sortDic = new Dictionary<string, Dictionary<string, double>>();
            foreach (string res in dic.Keys)
            {
                Dictionary<string, double>  neghborDic = nearestNeighbor(res, dic);
                sortDic.Add(res, neghborDic);
            }
            //取出最相近的K个，把这些用户评分高并且推荐目标没有用过的资源作为推荐项



            return "";
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
                if (dic2.Keys.Contains(res))
                {
                    distance += Math.Abs(dic1[res] - dic2[res]);
                }
            }
            return distance;
        }

        //    def computeNearestNeighbor(username, users):
        //"""计算所有用户至username用户的距离，倒序排列并返回结果列表"""
        //distances = []
        //for user in users:
        //    if user != username:
        //        distance = manhattan(users[user], users[username])
        //        distances.append((distance, user))
        //# 按距离排序——距离近的排在前面
        //distances.sort()
        //return distances

        /// <summary>
        /// 找出某个用户同其他用户的距离并排序
        /// </summary>
        /// <param name="techID"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public Dictionary<string, double> nearestNeighbor(string techID, Dictionary<string, Dictionary<string, double>> dic)
        {
            //List<Dictionary<string, double>> list = new List<Dictionary<string, double>>();
            Dictionary<string, double> disDic = new Dictionary<string, double>();

            foreach (string res in dic.Keys)
            {
                if (techID != res)
                {
                    double distance = manhattan(dic[techID],dic[res]);
                    disDic.Add(res, distance);
                }
                //list.Add(disDic);
            }
            //list.Sort();
            var disDicSort = from d in disDic
                          orderby d.Value
                          ascending
                          select d;
            return disDic;
        }


    }
}