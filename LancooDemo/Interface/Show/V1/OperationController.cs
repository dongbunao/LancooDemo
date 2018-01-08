using LancooDemo.BLL;
using System.Web.Http;

namespace LancooDemo.Interface.Show.V1
{
    public class OperationController : ApiController
    {
        [HttpGet]
        [ActionName("operationDistance")]
        public int operationDistance()
        {
            OperationBLL opt = new OperationBLL();
            int result = opt.operationDistance();
            return result;
        }
    }
}