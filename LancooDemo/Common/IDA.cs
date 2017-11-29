using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LancooDemo.Common
{
    public abstract class IDA
    {
        protected SqlHelper db;

        public IDA(SqlHelper db)
        {
            this.db = db;
        }


        protected IDataParameter ReturnParam(SqlDbType dbType)
        {
            IDataParameter param = new SqlParameter("@Return", dbType);
            param.Direction = ParameterDirection.ReturnValue;
            return param;
        }


        protected Parameters Param()
        {
            return new Parameters();
        }


        protected class Parameters
        {
            List<IDataParameter> parameters;

            internal Parameters()
            {
                parameters = new List<IDataParameter>();
            }

            public Parameters Add(string paramName, object value, SqlDbType dbType, int size)
            {
                SqlParameter p = new SqlParameter(paramName, dbType, size);
                p.Value = value;
                parameters.Add(p);
                return this;
            }

            public Parameters Add(string paramName, object value, SqlDbType dbType)
            {
                SqlParameter p = new SqlParameter(paramName, dbType);
                p.Value = value;
                parameters.Add(p);
                return this;
            }


            public Parameters Add(string paramName, object value)
            {
                parameters.Add(new SqlParameter(paramName, value));
                return this;
            }


            public List<IDataParameter> Build()
            {
                return parameters;
            }
        }

    }
}