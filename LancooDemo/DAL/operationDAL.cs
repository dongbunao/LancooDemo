using LancooDemo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LancooDemo.DAL
{
    /// <summary>
    /// 根据教师的资源使用次数构建评分表，计算教师之间的相似度
    /// </summary>
    public class OperationDAL : IDA
    {
        public OperationDAL(SqlHelper db) : base(db)
        {
        }
        //public string operationDistance()
        //{
        //    //1、取出所有老师的ID
        //    List<string> techIDs = this.getAllTechID();


        //    //2、构造每个老师的评分表
        //    Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
        //    dic = this.makeMarkTable(techIDs);

        //    //3、计算距离（数据稀疏-->余弦相似度；分数膨胀-->皮尔逊；稀疏且膨胀-->修正余弦相似度）
        //    //(1).计算每个老师对资源打分的平均值
            
        //    Dictionary<string, string> dicTemp = new Dictionary<string, string>();
        //    foreach (string tID in dic.Keys)
        //    {
        //        int sum = 0;
        //        Dictionary<string, double> dicAveg = new Dictionary<string, double>();
        //        //遍历教师tID对资源的评分
        //        foreach (string res in dic[tID].Keys)
        //        {
        //            sum = sum + int.Parse(dic[tID][res]);
        //            double aveg = sum / dic[tID].Count;
        //            dicAveg.Add(tID,aveg);
        //        } 

        //    }



        //    return "";
        //}

        /// <summary>
        /// 获取所有的教师ID
        /// </summary>
        /// <returns>techIDs 中存放所有教师的ID </returns>
        public List<string> getAllTechID()
        {
            List<string> techIDs = new List<string>();
            string sqlStr = "select UserID from Base_Teacher";

            DataTable dt = new DataTable();
            List<IDataParameter> parameters = Param()
               .Build();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);

            foreach (DataRow row in dt.Rows)
            {
                techIDs.Add(row.Value("UserID", ""));
            }

            return techIDs;
        }

        //获取一个老师对资源的评分
        public Dictionary<string, double> getScoreByTechID(string techID)
        {
            string sqlStr = "select ResourceID, Times from dbo.User_Use where UserID = '"+ techID + "' order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            Dictionary<string, double> dic = new Dictionary<string, double>();

            if (null == dt || dt.Rows.Count <= 0) return dic;

            foreach (DataRow row in dt.Rows)
            {
                dic.Add(row.Value("ResourceID", ""), row.Value("Times", 0));
            }

            return dic;
        }



        /// <summary>
        /// 构建教师对资源的评分表。例如   张三：{ 资源甲：3，资源乙：4 }
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, double>> makeMarkTable(List<string> list)
        {
            Dictionary<string, Dictionary<string, double>> dic = new Dictionary<string, Dictionary<string, double>>();

            for (int i = 0; i < list.Count; i++)
            {
                dic.Add(list[i], this.getScoreByTechID(list[i]));
            }
            return dic;
        }


    }
}