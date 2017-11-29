using LancooDemo.Common;
using LancooDemo.DAL;
using System;
using System.Collections.Generic;

namespace LancooDemo.BLL
{
    public class Transer_Tesource2BLL
    {
        /// <summary>
        /// 获取所有的记录
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetAll()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Transer_Resource2DAL(sh).GetAll();
        }

        /// <summary>
        /// 获取点击次数大于95次的记录
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetHeatRes()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Transer_Resource2DAL(sh).GetHeatRes();
        }

        /// <summary>
        /// 入库超过一定时间，使用次数少于某个值 ------> 建议检查或者删除这些资源
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetLongTimeNoUse()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Transer_Resource2DAL(sh).GetLongTimeNoUse();
        }

        /// <summary>
        /// 小学阶段的资源总数
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetPrimaryAll()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Transer_Resource2DAL(sh).GetPrimaryAll();
        }

        /// <summary>
        /// 小学阶段的资源并且存在媒体资料
        /// </summary>
        /// <returns></returns>
        public List<Models.Transer_Resource2Model> GetPrimaryRes()
        {
            SqlHelper sh = new SqlHelper();
            sh.Open();
            return new Transer_Resource2DAL(sh).GetPrimaryRes();
        }


    }
}