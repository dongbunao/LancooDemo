using LancooDemo.Common;
using LancooDemo.DAL;
using LancooDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LancooDemo.BLL
{
    public class Resource_GetResListBLL
    {
        public int GetResListByTime()
        {
            SqlHelper1 sh = new SqlHelper1();
            sh.Open();
            Resource_GetResListDAL dal = new Resource_GetResListDAL(sh);
            SqlHelper db = new SqlHelper();
            db.Open();
            Transer_Resource2DAL trdal = new Transer_Resource2DAL(db);
            trdal.DeleteResList();
            DataTable dt = dal.GetResListByTime();
            int res = 1;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    Transer_Resource2Model model = new Transer_Resource2Model();
                    model._RESOURCE_ID = row.Value("RESOURCE_ID", "");
                    model._RESOURCE_NAME = row.Value("RESOURCE_NAME", ""); 
                    model._RESOURCE_TYPE = row.Value("RESOURCE_TYPE", 0.00M);
                    model._RESOURCE_LEVEL = row.Value("RESOURCE_LEVEL", "");
                    model._STORE_DATE = row.Value("STORE_DATE", DateTime.Now);
                    model._THEME_CODE = row.Value("THEME_CODE", "");
                    model._THEME_TEXT = row.Value("THEME_TEXT", "");
                    model._IMPOR_KN_CODE = row.Value("IMPOR_KN_CODE", "");
                    model._IMPOR_KN_TEXT = row.Value("IMPOR_KN_TEXT", "");
                    model._MAIN_KN_CODE = row.Value("MAIN_KN_CODE", "");
                    model._MAIN_KN_TEXT = row.Value("MAIN_KN_TEXT", "");
                    model._UNIT_NUM = row.Value("UNIT_NUM", 0.00M);
                    model._RESOURCE_SIZE = row.Value("RESOURCE_SIZE", 0.00M);
                    model._ABANDON_NUM = row.Value("ABANDON_NUM", "");
                    model._APPLY_TOTAL_TIME = row.Value("APPLY_TOTAL_TIME", 0.00M);
                    model._ABANDON_RATE = row.Value("ABANDON_RATE", "");
                    model._DOWNLOAD_NUM = row.Value("DOWNLOAD_NUM", "");
                    model._SOURCE_LIBRARY = row.Value("SOURCE_LIBRARY", "");
                    model._RESOURCE_CLASS = row.Value("RESOURCE_CLASS", "");
                    model._MD5_CODE = row.Value("MD5_CODE", "");
                    model._INSTITU_UNIT = row.Value("INSTITU_UNIT", "");
                    model._RES_LENGTH = row.Value("RES_LENGTH", 0.00M);
                    model._DURATION_LENGTH = row.Value("DURATION_LENGTH", 0.00M);
                    model._FILE_PATH = row.Value("FILE_PATH", "");
                    model._FILE_CONTENT = row.Value("FILE_CONTENT", "");
                    model._IS_EXSIT_MEDIA = row.Value("IS_EXSIT_MEDIA", 0.00M);
                    model._DOWNLOAD_FLAG = row.Value("DOWNLOAD_FLAG", 0.00M);
                    model._SEQUENCE = row.Value("SEQUENCE", 0.00M);
                    model._HEAT_NUM = row.Value("HEAT_NUM", 0.00M);
                    model._SUBJECT_CODE = row.Value("SUBJECT_CODE", "");

                    

                    res = trdal.AddResListByTime(model);
                }
            }
            catch (Exception ex)
            {
                return -1;//内部报错
                //日志
            }
            db.Close();
            return res;
        }

    }

}