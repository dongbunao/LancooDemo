using LancooDemo.Common;
using LancooDemo.Models;
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
            string sqlStr = "insert into dbo.User_Use(UserID, ResourceID, Times, UpdateTime) select UserID, ResourceID, count(ResourceID) as Times, CONVERT(datetime,GETDATE()) from dbo.ResourceUse"
                             + " group by UserID, ResourceID having count(*) > 0 order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }

        public int InsertSubject_Use()
        {
            string sqlStr = "insert into dbo.Subject_Use(SubjectID, ResourceID, Times, UpdateTime) select SubjectID, ResourceID, count(SubjectID) as Times, CONVERT(datetime,GETDATE()) from dbo.ResourceUse"
                            + " group by SubjectID, ResourceID having count(*) > 0 order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }

        public int InsertResource_UseTotal()
        {
            string sqlStr = "insert into dbo.Resource_UseTotal(ResourceID, Times, UpdateTime) select ResourceID, count(ResourceID) as Times, CONVERT(datetime,GETDATE()) from dbo.ResourceUse"
                             + " group by ResourceID having count(*)>=0 order by Times desc";
            List<IDataParameter> parameters = Param()
                .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }

        public int InsertTeacher(TeacherModel model)
        {
            string sqlStr = "INSERT INTO [dbo].[Base_Teacher] ([Term],[UserID],[UserName],[Gender],[SchoolID],[SchoolName],[SubjectIDs],[SubjectNames])" 
                + "values(" +  model.Term + model.UserID + model.UserName + model.Gender + model.SchoolID + model.SchoolName + model.SubjectIDs + model.SubjectNames + ")";

            List<IDataParameter> parameters = Param()
               .Build();
            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);
            return effectRows;
        }


    }
}