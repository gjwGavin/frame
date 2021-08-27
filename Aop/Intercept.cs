using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Aop.Tool;
using WebApplication1.Common.Base;
using WebApplication1.Tool;
using NLog;

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
            //验证Token
            var tokenstr = filterContext.HttpContext.Request.Headers[ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField)];
            string message;
            TokenTool.ValidateJwtToken(tokenstr, TokenTool.TokenKey, out message);
            // if (!message.Equals("good")) throw new Exception(message);
            //if (!message.Equals("good")) filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Error", action = "notToken" }));
            
            
            
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {          
            
            if (filterContext.Exception!=null)
            {
                //记录日志
                

               
            }


            var ttt = filterContext.Result as ContentResult;
           // var tt2t = ttt as ParamBesa;
            
        }


    }
}