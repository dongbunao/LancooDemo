using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LancooDemo.Interface.Show.V1
{
    public class Transer_Resource2Controller : ApiController
    {
        /// <summary>
        /// 1、按百分比加权重计算
        /// 2、按照每项加减固定的分值进行计算
        /// 3、按照优良中差的等级
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("PingGu")]
        public string PingGu()
        {
            int a = this.GetAll().Count;  //总的记录数
            int z = this.GetPrimaryAll().Count; //小学阶段的所有资源数量
            int b = this.GetHeatRes().Count;  //热点资源数量
            int c = this.GetLongTimeNoUse().Count;  //入库超过一定时间，使用次数少于某个值的资源数量
            int d = this.GetPrimaryRes().Count;  //小学阶段的资源并且不存在媒体资料


            float b1 = (float)Math.Round((float)b / a, 3);
            float c1 = (float)Math.Round((float)c / a, 3);
            float d1 = (float)Math.Round((float)d / a, 3);

            #region  1、按百分比加权重计算
            //float wb1 = 8, wc1 = -10, wd1 = 5;  //各维度权重
            //float ww = wb1 + wc1 + wd1;  //权重总值
            ////质量的绝对量化值
            //float SA = (float)Math.Round((b1 * wb1 + c1 * wc1 + d1 * wd1) / ww, 3);
            //string StrSA = (SA * 100).ToString();
            #endregion

            #region  2、按照每项加减固定的分值进行计算
            float sum = 100.0f;
            if (b1 <= 0.5) { sum = sum - (b1 * 10); }
            if (c1 >= 0) { sum = sum - (c1 * 10); }
            if (d1 >= 0.5) { sum = sum - (d1 * 10); }
            string StrSA = sum.ToString();
            #endregion


            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("数据质量值", StrSA);
            dic.Add("这些资源比较热，建议增加同类资源", Newtonsoft.Json.JsonConvert.SerializeObject(this.GetHeatRes()[0]));
            dic.Add("这些资源建议删除", Newtonsoft.Json.JsonConvert.SerializeObject(this.GetLongTimeNoUse()[0]));
            dic.Add("这些资源建议增加多媒体资源", Newtonsoft.Json.JsonConvert.SerializeObject(this.GetPrimaryRes()[0]));

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(dic);

            return result;
        }




        [HttpGet]
        [ActionName("GetAll")]
        public List<Models.Transer_Resource2Model> GetAll()
        {
            BLL.Transer_Tesource2BLL bll = new BLL.Transer_Tesource2BLL();
            string ii = Newtonsoft.Json.JsonConvert.SerializeObject(bll.GetAll());
            return bll.GetAll();
        }

        [HttpGet]
        [ActionName("GetPrimaryAll")]
        public List<Models.Transer_Resource2Model> GetPrimaryAll()
        {
            BLL.Transer_Tesource2BLL bll = new BLL.Transer_Tesource2BLL();
            return bll.GetPrimaryAll();
        }


        [HttpGet]
        [ActionName("GetHeatRes")]
        public List<Models.Transer_Resource2Model> GetHeatRes()
        {
            BLL.Transer_Tesource2BLL bll = new BLL.Transer_Tesource2BLL();
            return bll.GetHeatRes();
        }

        [HttpGet]
        [ActionName("GetLongTimeNoUse")]
        public List<Models.Transer_Resource2Model> GetLongTimeNoUse()
        {
            BLL.Transer_Tesource2BLL bll = new BLL.Transer_Tesource2BLL();
            return bll.GetLongTimeNoUse();
        }

        [HttpGet]
        [ActionName("GetPrimaryRes")]
        public List<Models.Transer_Resource2Model> GetPrimaryRes()
        {
            BLL.Transer_Tesource2BLL bll = new BLL.Transer_Tesource2BLL();
            return bll.GetPrimaryRes();
        }


    }
}