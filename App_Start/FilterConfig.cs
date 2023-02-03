using System.Web;
using System.Web.Mvc;
using WebApplication1.Aop;

namespace WebApplication1
{
    public class FilterConfig
    {
        /// <summary>
        /// http://t.zoukankan.com/tuqunfu-p-15630134.html
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Intercept());
            filters.Add(new HandleError());
        }
    }
}
