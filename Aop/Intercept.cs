using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Aop.Tool;
using WebApplication1.Common;
using WebApplication1.Common.Base;
using WebApplication1.Tool;

namespace WebApplication1.Aop
{

    /// <summary>
    /// 自定义AuthorizeAttribute过滤
    /// </summary>  
    public class Intercept : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            SeriLogTool.Debug($"{controllName}/{actionName}    参数： {JsonConvert.SerializeObject(filterContext.ActionParameters)}");
            //过滤请求            
            //不需要过滤的请求
            if (ActionList.IsNotAction(controllName, actionName)) return;

            //进行过滤拦截验证
            //验证Token
            var tokenstr = filterContext.HttpContext.Request.Headers[ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField)];
            string message;
            TokenTool.ValidateJwtToken(tokenstr, TokenTool.TokenKey, out message);
            //if (!message.Equals("good")) throw new Exception(message);
            //if (!message.Equals("good")) filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Error", action = "notToken" }));



        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string controllName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            if (!ActionList.IsNotAction(controllName, actionName))
            {
                //解析前端验证过的token 然后修改生命周期 再返回给前端 保证Token不失效
                var token = filterContext.HttpContext.Request.Headers[ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField)];
                var TokenInfo = TokenTool.Validate(token);
                var Newtoken = TokenTool.SaveHeader(new TokenTool.TokenInfo() { UserID = TokenInfo["UserID"].ToString(), UserName = TokenInfo["UserName"].ToString() });
                filterContext.HttpContext.Response.AddHeader("Token", Newtoken);
            }
            //记录日志
            var StatusCode = filterContext.HttpContext.Response.StatusCode;
            if (filterContext.Exception != null)
            {
                SeriLogTool.Error($"{controllName}/{actionName}出现了错误 错误码 {StatusCode} 错误信息: {filterContext.Exception.Message} \r\n {filterContext.Exception.StackTrace}");
            }




        }

    }
}