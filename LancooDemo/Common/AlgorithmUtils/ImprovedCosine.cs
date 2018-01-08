using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LancooDemo.Common.AlgorithmUtils
{
    /// <summary>
    /// 修正余弦相似度的相关计算
    /// </summary>
    public class ImprovedCosine
    {
        public double getDistance(Dictionary<string, Dictionary<string, double>> dic, string bound1, string bound2)
        {
            //(1).计算每个老师对资源打分的平均值   这部分存在问题
            Dictionary<string, double> dicAveg = new Dictionary<string, double>();
            foreach (string tID in dic.Keys)
            {
                double sum = 0.0;
                //遍历教师tID对资源的评分
                foreach (string res in dic[tID].Keys)
                {
                    sum = sum + dic[tID][res];
                }
                double aveg = sum / dic[tID].Count;

                dicAveg.Add(tID, aveg);
            }
            double num = 0.0; // 分子
            double dem1 = 0.0; // 分母的第一部分
            double dem2 = 0.0; // 分母的第二部分

            foreach (string tID in dic.Keys)
            {

                if (dic[tID].ContainsKey(bound1) && dic[tID].ContainsKey(bound2))
                {
                    double avg = dicAveg[tID];
                    num += (dic[tID][bound1] - avg) * (dic[tID][bound2] - avg);
                    dem1 += (dic[tID][bound1] - avg)* (dic[tID][bound1] - avg);
                    dem2 += (dic[tID][bound2] - avg) * (dic[tID][bound2] - avg);
                }
            }
            return num / (Math.Sqrt(dem1) * Math.Sqrt(dem2));

        }

    }
}