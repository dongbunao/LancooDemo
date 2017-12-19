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

            Dictionary<string, string> dicTemp = new Dictionary<string, string>();
            foreach (string tID in dic.Keys)
            {
                double sum = 0.0;
                Dictionary<string, double> dicAveg = new Dictionary<string, double>();
                //遍历教师tID对资源的评分
                foreach (string res in dic[tID].Keys)
                {
                    sum = sum + dic[tID][res];
                    double aveg = sum / dic[tID].Count;
                    dicAveg.Add(tID, aveg);
                }

            }



            return "";
        }


    }
}