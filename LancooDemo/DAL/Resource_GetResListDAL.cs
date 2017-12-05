﻿using LancooDemo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LancooDemo.DAL
{
    public class Resource_GetResListDAL : IDA1
    {
        public Resource_GetResListDAL(SqlHelper1 db) : base(db)
        {
        }

        public DataTable GetResListByTime()
        {

            List<IDataParameter> parameters = Param()
                //.Add("@courseCode", "")
                //.Add("@versionCode", "")
                //.Add("@type", "")
                //.Add("@pageIndex", 1)
                .Add("@pageSize", int.MaxValue)
                //.Add("@totalCount", )
                //.Add("@filter", "")
                .Build();
            string sqlStr = "P_Lgdb_Resource_GetResList";
            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.StoredProcedure, parameters);

            return dt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UPLocalDataByTime()
        {   //1、删除表中原有数据
            //2、统计（计算）后的数据插入表中
            string sqlStr = "P_Lgdb_Resource_GetResList";
            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.StoredProcedure, parameters);

        }




    }
}