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
            BLL.UpdateLocalDataBLL upBll = new BLL.UpdateLocalDataBLL();
            upBll.UpdateByTime();
            BLL.OperationBLL optBll = new BLL.OperationBLL();
            optBll.operationDistance();
        }
        }
}