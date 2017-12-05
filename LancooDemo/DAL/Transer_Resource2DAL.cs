using LancooDemo.Common;
using LancooDemo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace LancooDemo.DAL
{
    public class Transer_Resource2DAL : IDA
    {
        public Transer_Resource2DAL(SqlHelper db) : base(db)
        {
        }

        /// <summary>
        /// 获取所有的记录
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetAll()
        {
            string sqlStr = "select RESOURCE_ID, RESOURCE_NAME from dbo.Transer_Resource2";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0) return null;

            List<Models.Transer_Resource2Model> infoList = new List<Models.Transer_Resource2Model>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Transer_Resource2Model()
                {
                    _RESOURCE_ID = row.Value("RESOURCE_ID", ""),
                    _RESOURCE_NAME = row.Value("RESOURCE_NAME", ""),

                });
            }
            return infoList;
        }

        /// <summary>
        /// 点击次数大于95的记录 ------>建议增加同类资源
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetHeatRes()
        {
            string sqlStr = "select RESOURCE_ID, RESOURCE_NAME, HEAT_NUM from dbo.Transer_Resource2 where HEAT_NUM > 95";
            //string sqlStr = "select Top 1 [RESOURCE_ID],[RESOURCE_NAME],[HEAT_NUM] from dbo.Transer_Resource2 where HEAT_NUM > 95";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0)
                return null;

            List<Models.Transer_Resource2Model> infoList = new List<Models.Transer_Resource2Model>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Transer_Resource2Model()
                {
                    _RESOURCE_ID = row.Value("RESOURCE_ID", ""),
                    _RESOURCE_NAME = row.Value("RESOURCE_NAME", ""),
                    _HEAT_NUM = row.Value("HEAT_NUM", 0),
                });
            }

            return infoList;
        }

        /// <summary>
        /// 入库超过一定时间，使用次数少于某个值 ------> 建议检查或者删除这些资源
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetLongTimeNoUse()
        {
            string sqlStr = "select * from dbo.Transer_Resource2 where STORE_DATE < DATEADD(DAY,-60,GETDATE()) and APPLY_TOTAL_TIME<30 order by STORE_DATE asc";
            // string sqlStr = "select TOP 1 [RESOURCE_ID],[RESOURCE_NAME],[STORE_DATE],[APPLY_TOTAL_TIME] from dbo.Transer_Resource2 where STORE_DATE < DATEADD(DAY,-60,GETDATE()) and APPLY_TOTAL_TIME<30 order by STORE_DATE asc";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0)
            {
                return null;
            }

            List<Models.Transer_Resource2Model> infoList = new List<Models.Transer_Resource2Model>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Transer_Resource2Model()
                {
                    _RESOURCE_ID = row.Value("RESOURCE_ID", ""),
                    _RESOURCE_NAME = row.Value("RESOURCE_NAME", ""),
                    _STORE_DATE = row.Value("STORE_DATE", System.DateTime.Now),
                    _APPLY_TOTAL_TIME = row.Value("APPLY_TOTAL_TIME", 0),
                });
            }
            return infoList;
        }

        /// <summary>
        /// 小学阶段的所有资源数
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetPrimaryAll()
        {
            string sqlStr = "select * from dbo.Transer_Resource2 where RESOURCE_LEVEL = 'A'";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0)
            {
                return null;
            }

            List<Models.Transer_Resource2Model> infoList = new List<Models.Transer_Resource2Model>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Transer_Resource2Model()
                {
                    _RESOURCE_ID = row.Value("RESOURCE_ID", ""),
                    _RESOURCE_NAME = row.Value("RESOURCE_NAME", ""),
                    _STORE_DATE = row.Value("STORE_DATE", System.DateTime.Now),
                    _APPLY_TOTAL_TIME = row.Value("APPLY_TOTAL_TIME", 0),
                    _IS_EXSIT_MEDIA = row.Value("IS_EXSIT_MEDIA", 0),
                    _RESOURCE_LEVEL = row.Value("RESOURCE_LEVEL", ""),
                });
            }
            return infoList;
        }

        /// <summary>
        /// 小学阶段的资源并且不存在媒体资料 ------> 
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetPrimaryRes()
        {
            string sqlStr = "select * from dbo.Transer_Resource2 where IS_EXSIT_MEDIA = 1 and RESOURCE_LEVEL = 'A'";
            // string sqlStr = "select TOP 1 [RESOURCE_ID],[RESOURCE_NAME],[STORE_DATE],[APPLY_TOTAL_TIME],[IS_EXSIT_MEDIA],[RESOURCE_LEVEL] from dbo.Transer_Resource2 where IS_EXSIT_MEDIA = 0 and RESOURCE_LEVEL = 'A'";
            List<IDataParameter> parameters = Param()
                .Build();

            DataTable dt = new DataTable();
            dt = db.QueryCommand(sqlStr, CommandType.Text, parameters);
            if (null == dt || dt.Rows.Count <= 0)
            {
                return null;
            }

            List<Models.Transer_Resource2Model> infoList = new List<Models.Transer_Resource2Model>();
            foreach (DataRow row in dt.Rows)
            {
                infoList.Add(new Models.Transer_Resource2Model()
                {
                    _RESOURCE_ID = row.Value("RESOURCE_ID", ""),
                    _RESOURCE_NAME = row.Value("RESOURCE_NAME", ""),
                    _STORE_DATE = row.Value("STORE_DATE", System.DateTime.Now),
                    _APPLY_TOTAL_TIME = row.Value("APPLY_TOTAL_TIME", 0),
                    _IS_EXSIT_MEDIA = row.Value("IS_EXSIT_MEDIA", 0),
                    _RESOURCE_LEVEL = row.Value("RESOURCE_LEVEL", ""),
                });
            }
            return infoList;
        }


        /// <summary>
        /// 更新资源库的资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddResListByTime(Transer_Resource2Model model)
        {
            string sqlStr = "Transer_Resource2AddByTime";
            List<IDataParameter> parameters = Param()
                .Add("@RESOURCE_ID", model._RESOURCE_ID)
                .Add("@RESOURCE_NAME", model._RESOURCE_NAME)
                .Add("@RESOURCE_TYPE", model._RESOURCE_TYPE)
                .Add("@RESOURCE_LEVEL", model._RESOURCE_LEVEL)
                .Add("@STORE_DATE", model._STORE_DATE)
                .Add("@THEME_CODE", model._THEME_CODE)
                .Add("@THEME_TEXT", model._THEME_TEXT)
                .Add("@IMPOR_KN_CODE", model._IMPOR_KN_CODE)
                .Add("@IMPOR_KN_TEXT", model._IMPOR_KN_TEXT)
                .Add("@MAIN_KN_CODE", model._MAIN_KN_CODE)
                .Add("@MAIN_KN_TEXT", model._MAIN_KN_TEXT)
                .Add("@UNIT_NUM", model._UNIT_NUM)
                .Add("@RESOURCE_SIZE", model._RESOURCE_SIZE)
                .Add("@ABANDON_NUM", model._ABANDON_NUM)
                .Add("@APPLY_TOTAL_TIME", model._APPLY_TOTAL_TIME)
                .Add("@ABANDON_RATE", model._ABANDON_RATE)
                .Add("@DOWNLOAD_NUM", model._DOWNLOAD_NUM)
                .Add("@SOURCE_LIBRARY", model._SOURCE_LIBRARY)
                .Add("@RESOURCE_CLASS", model._RESOURCE_CLASS)
                .Add("@MD5_CODE", model._MD5_CODE)
                .Add("@INSTITU_UNIT", model._INSTITU_UNIT)
                .Add("@RES_LENGTH", model._RES_LENGTH)
                .Add("@DURATION_LENGTH", model._DURATION_LENGTH)
                .Add("@FILE_PATH", model._FILE_PATH)
                .Add("@FILE_CONTENT", model._FILE_CONTENT)
                .Add("@IS_EXSIT_MEDIA", model._IS_EXSIT_MEDIA)
                .Add("@DOWNLOAD_FLAG", model._DOWNLOAD_FLAG)
                .Add("@SEQUENCE", model._SEQUENCE)
                .Add("@HEAT_NUM", model._HEAT_NUM)
                .Add("@SUBJECT_CODE", model._SUBJECT_CODE)
                .Build();


            int effectRows = db.RunCommand(sqlStr, CommandType.StoredProcedure, parameters);

            return effectRows;
        }


    }
}