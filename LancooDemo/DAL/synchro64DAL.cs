using LancooDemo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LancooDemo.DAL
{
    public class Synchro64DAL : IDA2
    {
        public Synchro64DAL(SqlHelper2 db) : base(db)
        {
        }

        public DataTable SyncTeacher()
        {
            string sqlStr = "select * from dbo.Base_Teacher"; 

            DataTable dt = new DataTable();
            List<IDataParameter> parameters = Param()
               .Build();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            return dt;
        }


    }
}