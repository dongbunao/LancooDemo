using LancooDemo.Common;
using LancooDemo.DAL;
using LancooDemo.Models;
using System;
using System.Data;

namespace LancooDemo.BLL
{
    public class Synchro64BLL
    {
        /// <summary>
        /// 把64服务器上byjiang数据库中的教师同步到本地Base_Teacher表中
        /// </summary>
        /// <returns></returns>
        public int SynochroTeacher()
        {
            SqlHelper2 sh2 = new SqlHelper2();
            sh2.Open();

            Synchro64DAL sy64 = new Synchro64DAL(sh2);
            DataTable dt = sy64.SyncTeacher();

            SqlHelper sh = new SqlHelper();
            sh.Open();

            InsertNewDAL insertNewDAL = new InsertNewDAL(sh); 

            int res = 0;
            foreach (DataRow row in dt.Rows)
            {
                TeacherModel model = new TeacherModel();
                model.Term = row.Value("Term","");
                model.UserID = row.Value("UserID", "");
                model.UserName = row.Value("UserName", "");
                model.Gender = row.Value("Gender", "");
                model.SchoolID = row.Value("SchoolID", "");
                model.SchoolName = row.Value("SchoolName", "");
                model.SubjectIDs = row.Value("SubjectIDs", "");
                model.SubjectNames = row.Value("SubjectNames", "");
                model.TS = row.Value("TS", DateTime.Now);

                res = insertNewDAL.InsertTeacher(model);

            }

            sh2.Close();
            sh.Close();

            return res;
        }
       
    }
}