
using System.Collections.Generic;
using System.Web.Http;

namespace LancooDemo.Interface.Show.V1
{
    public class Recommend_ResourceController : ApiController
    {
        /// <summary>
        /// 根据UserID查询用户的使用资源记录
        /// </summary>
        /// <param name="myUserModels"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetAll")]
        public string GetAll(Models.MyUserModels myUserModels)
        {
            if (myUserModels == null || myUserModels.UId == null || myUserModels.UId == "")
            {
                return "error：用户名不对！";
            }
            else
            {
                BLL.Recommend_ResouceBLL bll = new BLL.Recommend_ResouceBLL();
                string ii = Newtonsoft.Json.JsonConvert.SerializeObject(bll.GetAll(myUserModels.UId));
                return ii;
            }
        }


        [HttpPost]
        [ActionName("ListUnion")]
        public string ListUnion(Models.MyUserModels myUserModels)
        {
            if (myUserModels == null || myUserModels.UId == null || myUserModels.UId == "")
            {
                return "error：用户名不对！";
            }
            else
            {
                BLL.Recommend_ResouceBLL bll = new BLL.Recommend_ResouceBLL();
                string ii = Newtonsoft.Json.JsonConvert.SerializeObject(bll.ListUnion(myUserModels.UId));

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("您可能需要这些资源", ii);
                string result = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
                return result;
            }

        }

        [HttpGet]
        [ActionName("ListUnionGet")]
        public string ListUnionGet()
        {
            
                BLL.Recommend_ResouceBLL bll = new BLL.Recommend_ResouceBLL();
                string ii = Newtonsoft.Json.JsonConvert.SerializeObject(bll.ListUnion("sa16045"));

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("您可能需要这些资源", ii);
                string result = Newtonsoft.Json.JsonConvert.SerializeObject(dic);
                return result;
            

        }


    }
}