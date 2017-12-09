using LancooDemo.Common;
using LancooDemo.DAL;
using System;

namespace LancooDemo.BLL
{
    public class UpdateLocalDataBLL
    {
        public int UpdateByTime()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();

            DeleteOldDAL dlt = new DeleteOldDAL(sh);
            InsertNewDAL ins = new InsertNewDAL(sh);

            try
            {   // 把本地的中间结果数据表进行更新（先清空后插入）
                dlt.DeleteUserUse();
                ins.InsertUesr_Use();

                dlt.DeleteSubject_Use();
                ins.InsertSubject_Use();

                dlt.DeleteResource_UseTotal();
                ins.InsertResource_UseTotal();
            }
            catch (Exception ex)
            {
                return 0;   //更新数据失败
            }

            return 1;   //跟新数据成功
        }

    }
}