using LancooDemo.Common;
using LancooDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LancooDemo.BLL
{
    public class Recommend_ResouceBLL
    {
        /// <summary>
        /// 查询用户的所有资源使用记录
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<string> GetAll(string UserID)
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Recommend_ResourceDAL(sh).GetTop(UserID);
        }

        /// <summary>
        /// 合并个人TOP和相似TOP，去除重复。所得到的结果可作为推荐选项
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<string> ListUnion(string UserID)
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Recommend_ResourceDAL(sh).ListUnion(UserID);
        }
    }
}