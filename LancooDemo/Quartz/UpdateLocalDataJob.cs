using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LancooDemo.Quartz
{
    public class UpdateLocalDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            BLL.UpdateLocalDataBLL bll = new BLL.UpdateLocalDataBLL();
            bll.UpdateByTime();
        }
        }
}