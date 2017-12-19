using LancooDemo.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LancooDemo.Interface.Show.V1
{
    public class UpdateLocalDataController : ApiController
    {

        [HttpGet]
        [ActionName("UpdateByTime")]
        public int UpdateByTime()
        {
            UpdateLocalDataBLL uld = new UpdateLocalDataBLL();
             int result = uld.UpdateByTime();
            return result;
        }

        [HttpGet]
        [ActionName("Update64Teach")]
        public int Update64Teach()
        {
            Synchro64BLL sy64 = new Synchro64BLL();
            int result = sy64.SynochroTeacher();
            return result;
        }


    }
}