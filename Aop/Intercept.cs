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
            //if (!message.Equals("good")) throw new Exception(message);
            //if (!message.Equals("good")) filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Error", action = "notToken" }));



        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (filterContext.Exception != null)
            {
                string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}00_Logs\{LogEvent}\log.log";
                string SerilogOutputTemplate = "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 50);

                Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .MinimumLevel.Debug() // 所有Sink的最小记录级别
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .CreateLogger();
                string controllName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = filterContext.ActionDescriptor.ActionName;
               // filterContext.HttpContext.Request;
                Log.Information(string.Format(controllName+ "/"+actionName));
                Log.Error(string.Format(controllName + "/" + actionName)+ filterContext.Exception.Message);

            }


        }




    }
}