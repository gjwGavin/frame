using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Common.Base
{
    public class ParamBesa
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool Successful { set; get; } = true;     
    }
}