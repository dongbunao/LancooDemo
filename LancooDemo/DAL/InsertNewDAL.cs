using LancooDemo.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LancooDemo.DAL
{
    public class InsertNewDAL : IDA
    {
        public InsertNewDAL(SqlHelper db) : base(db)
        {
        }

        public int InsertUesr_Use()
        {
            string sqlStr = "insert into dbo.User_Use select UserID, ResourceID, count(ResourceID) as Times from dbo.ResourceUse"
                             + " group by UserID, ResourceID having count(*) > 0 order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }

        public int InsertSubject_Use()
        {
            string sqlStr = "insert into dbo.Subject_Use select SubjectID, ResourceID, count(SubjectID) as Times from dbo.ResourceUse" 
                            + " group by SubjectID, ResourceID having count(*) > 0 order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }

        public int InsertResource_UseTotal()
        {
            string sqlStr = "insert into dbo.Resource_UseTotal select ResourceID, count(ResourceID) as Times from dbo.ResourceUse"
                             + " group by ResourceID having count(*)>=0 order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }

    }
}