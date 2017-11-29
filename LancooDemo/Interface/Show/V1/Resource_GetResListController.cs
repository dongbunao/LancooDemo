using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LancooDemo.Interface.Show.V1
{
    public class Resource_GetResListController : ApiController
    {
        [HttpPost]
        [ActionName("GetAllRes")]
        public int GetAllRes()
        {
            BLL.Resource_GetResListBLL bll = new BLL.Resource_GetResListBLL();
            int res = bll.GetResListByTime();
            return res;
        }
    }
}