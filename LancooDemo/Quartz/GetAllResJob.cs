using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LancooDemo.Quartz
{
    public class GetAllResJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            BLL.Resource_GetResListBLL bll = new BLL.Resource_GetResListBLL();
          bll.GetResListByTime();
        }
        }
}