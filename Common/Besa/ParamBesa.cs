using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Common.Base
{
    public class BesaBO
    {
        public int code { set; get; } = 20000;
        /// <summary>
        /// 错误信息
        /// </summary>
        public string message { set; get; } = "执行成功！";

        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool Successful { set; get; } = true;
    }

    public class ReturnBesa : BesaBO
    {
        /// <summary>
        /// Token
        /// </summary>
        public string token { set; get; }
    }
    public class ReturnValueBesa<T> : BesaBO
    {
        /// <summary>
        /// 自定义值
        /// </summary>
        public T Value { set; get; }
    }
}