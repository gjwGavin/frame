using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace WebApplication1.Aop
{    
    public class Intercept : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            //过滤请求            
            //不需要过滤的请求
            if (controllName.Equals("Login") && actionName.Equals("Login")) return;
            //进行过滤拦截验证





            
        }
        

    }
}