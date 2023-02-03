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
            // ��Ӧ�ó�������ʱ���еĴ���
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters); //��Global.asax��Application_Start������GlobalFiltersע��
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //������
            var app = ((System.Web.HttpApplication)sender);
            if (!string.IsNullOrWhiteSpace(app.Request.Headers["Origin"]))
            {
                app.Response.AppendHeader("Access-Control-Allow-Credentials", "true");
                app.Response.AppendHeader("Access-Control-Allow-Origin", app.Request.Headers["Origin"]);//Ŀǰ�����еĵ�ַ�����Է��ʣ������Ҫָ���̶���ַ�Ļ�������������ָ����Ҳ����д�������ļ��н��п���
                if (!string.IsNullOrWhiteSpace(app.Request.Headers["Access-Control-Request-Method"]))
                {
                    app.Response.AppendHeader("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS, POST, PUT");
                    app.Response.AppendHeader("Access-Control-Expose-Headers", ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField));
                    //app.Response.Headers.Set("Access-Control-Expose-Headers", ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField));
                    //����ǰ�˵��ֶ� �������һ��UID
                    string configstr = "cache-control,content-type,if-modified-since,origin,x-requested-with,content-language,auth,UID" + "," + ConfigTool.GetConfigValue(ConfigTool.ConfigField.TokenField);
                    app.Response.AppendHeader("Access-Control-Allow-Headers", configstr);
                    app.Response.End();
                }
            }
        }
    }
}
