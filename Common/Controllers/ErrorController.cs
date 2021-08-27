using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Common.Controllers
{
    /// <summary>
    /// 重定向捕捉异常
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: Error
        public string notLogin()
        {
            return ActionTool.ToParamBesa("用户未登录");
        }
        public string notToken()
        {
            return ActionTool.ToParamBesa("用户登录验证失败");
        }      
    }
}