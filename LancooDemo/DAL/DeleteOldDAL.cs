using LancooDemo.Common;
using System.Collections.Generic;
using System.Data;

namespace LancooDemo.DAL
{
    public class DeleteOldDAL : IDA
    {
        public DeleteOldDAL(SqlHelper db) : base(db)
        {
        }

        /// <summary>
        /// 同步资源库数据之前，清空本地资源数据
        /// </summary>
        /// <returns></returns>
        public int DeleteResList()
        {
            string sqlStr = "delete [dbo].[Transer_Resource3]";
            List<IDataParameter> parameters = Param()
                 .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);

            return effectRows;
        }

        public int DeleteUserUse()
        {
            string sqlStr = "delete [dbo].[User_Use]";
            List<IDataParameter> parameters = Param()
                 .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);

            return effectRows;
        }

        public int DeleteSubject_Use()
        {
            string sqlStr = "delete [dbo].[Subject_Use]";
            List<IDataParameter> parameters = Param()
                 .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);

            return effectRows;
        }

        public int DeleteResource_UseTotal()
        {
            string sqlStr = "delete [dbo].[Resource_UseTotal]";
            List<IDataParameter> parameters = Param()
                 .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);

            return effectRows;
        }

        public int DeleteBase_Teacher()
        {
            string sqlStr = "delete [dbo].[Base_Teacher]";
            List<IDataParameter> parameters = Param()
                 .Build();

            int effectRows = db.RunCommand(sqlStr, CommandType.Text, parameters);

            return effectRows;
        }


    }
}