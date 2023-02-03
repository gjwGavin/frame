using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.Tool;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); //在Global.asax的Application_Start方法中GlobalFilters注册
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //跨域处理
            var app = ((System.Web.HttpApplication)sender);
            if (!string.IsNullOrWhiteSpace(app.Request.Headers["Origin"]))
            {
                app.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
                app.Response.AppendHeader("Access-Control-Allow-Origin", app.Request.Headers["Origin"]);//目前是所有的地址都可以访问，如果需要指定固定地址的话，可以在这里指定，也可以写进配置文件中进行控制
                if (!string.IsNullOrWhiteSpace(app.Request.Headers["Access-Control-Request-Method"]))
                {
                    app.Response.AppendHeader("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS, POST, PUT");
                    app.Response.AppendHeader("Access-Control-Expose-Headers", ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField));
                    //app.Response.Headers.Set("Access-Control-Expose-Headers", ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField));
                    //允许前端的字段 比如加了一个UID
                    string configstr = "cache-control,content-type,if-modified-since,origin,x-requested-with,content-language,auth,UID" + "," + ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField);
                    app.Response.AppendHeader("Access-Control-Allow-Headers", configstr);
                    app.Response.End();
                }
            }
        }
    }
}
