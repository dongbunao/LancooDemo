using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LancooDemo.Common;
using LancooDemo.Models;

namespace LancooDemo.DAL
{
    public class Recommend_ResourceDAL : IDA
    {
        public Recommend_ResourceDAL(SqlHelper db) : base(db)
        {
        }

        /// <summary>
        /// 获取用户的信息（学科，年级，身份）
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public MyUserModels GetUserInfo(string UserID)
        {
            string uID = UserID;
            string sqlStr = "select * from dbo.ResourceUse where UserID = '" + uID + "' ";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0) return null;

            DataRow dtr = dt.Rows[0];
            MyUserModels myUser = new MyUserModels();

            myUser.SbujectID = dtr["SubjectID"].ToString();
            
            return myUser;
        }


        /// <summary>
        /// 查询用户的所有资源使用记录
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Models.Recommend_ResouceModel> GetAll(string UserID)
        {
            string uID = UserID;
            string sqlStr = "select * from dbo.ResourceUse where UserID = '" + uID + "' ";
            List<IDataParameter> parameters = Param()
                .Build();
             
            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0) return null;

            List<Models.Recommend_ResouceModel> infoList = new List<Models.Recommend_ResouceModel>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Recommend_ResouceModel()
                {
                    ResourceID = row.Value("ResourceID", ""),
                    //ResourceName = row.Value("ResourceName", ""),
                    //SubjectID = row.Value("SubjectID", ""),
                    //Type = row.Value("Type", ""),
                    //UseTime = row.Value("UseTime", 0.00),
                });
            }
            
      
            return infoList;
        }

        /// <summary>
        /// 用户资源使用排行  前20
        /// </summary>
        /// <returns></returns>
        public List<string> GetTop(string UserID)
        {
            string uID = UserID;
            string sqlStr = "select top 20 UserID, ResourceID, Times from dbo.User_Use where UserID = '"+uID+"' order by Times desc";
            //string sqlStr = "select TOP 20 [ResourceID],[ResourceName],[UseTime] from dbo.ResourceUse where UserID = '" + uID + "' order by UseTime desc";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0) return null;

            List<Models.Recommend_ResouceModel> infoList = new List<Models.Recommend_ResouceModel>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Recommend_ResouceModel()
                {
                    ResourceID = row.Value("ResourceID", ""),
                    //ResourceName = row.Value("ResourceName", ""),
                    //SubjectID = row.Value("SubjectID", ""),
                    //Type = row.Value("Type", ""),
                    //UseTime = row.Value("UseTime", 0.00),
                    //Time = row.Value("Time",DateTime.Now)
                });
            }

            List<string> resList = new List<string>();
            for (int i=0;i<infoList.Count();i++)
            {
                string tem = infoList[i].ResourceID;
                resList.Add(tem.Trim());
            }

            return resList;
        }

        /// <summary>
        /// 查询用户所属年级，班级，学科的资源使用排行  前20
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<string> GetSimTop(string UserID)
        {
            string uID = UserID;
            MyUserModels myUser = this.GetUserInfo(uID);
            string subID = myUser.SbujectID;
            string sqlStr = "select top 20 SubjectID, ResourceID, Times from dbo.Subject_Use where SubjectID = '"+ subID + "' order by Times desc";
            //string sqlStr = "select TOP 20 [ResourceID],[ResourceName],[UseTime] from dbo.ResourceUse where SubjectID = '" + subID + "' order by UseTime desc";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0) return null;

            List<Models.Recommend_ResouceModel> infoList = new List<Models.Recommend_ResouceModel>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Recommend_ResouceModel()
                {
                    ResourceID = row.Value("ResourceID", ""),
                    //ResourceName = row.Value("ResourceName", ""),
                    //SubjectID = row.Value("SubjectID", ""),
                    //Type = row.Value("Type", ""),
                    //UseTime = row.Value("UseTime", 0.00),
                    //Time = row.Value("Time", DateTime.Now)
                });
            }

            List<string> resList = new List<string>();
            for (int i = 0; i < infoList.Count(); i++)
            {
                string tem = infoList[i].ResourceID;
                resList.Add(tem.Trim());
            }

            return resList;
        }

        /// <summary>
        /// 合并个人TOP和相似TOP，去除重复。所得到的结果可作为推荐选项
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<string> ListUnion(string UserID)
        {
            string uID = UserID;
            List<string> l1 = this.GetSimTop(uID);
            List<string> l2 = this.GetTop(uID);

            //var result = l1.Union(l2, new MyComparer()).Except(l1.Intersect(l2, new MyComparer()), new MyComparer());

            List<string> l11 = l1.Union(l2).ToList<string>();  //并集
            List<string> l12 = l1.Intersect(l2).ToList<string>();//l1,l2都有
            List<string> l3 = l1.Except(l2).ToList<string>();  //l1有，l2没有
            return l3;
        }


    }
}