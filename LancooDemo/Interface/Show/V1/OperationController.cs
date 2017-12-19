using LancooDemo.BLL;
using System.Web.Http;

namespace LancooDemo.Interface.Show.V1
{
    public class OperationController : ApiController
    {
        [HttpGet]
        [ActionName("operationDistance")]
        public string operationDistance()
        {
            OperationBLL opt = new OperationBLL();
            string result = opt.operationDistance();
            return result;
        }
    }
}