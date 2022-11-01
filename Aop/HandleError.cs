using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Tool;

namespace WebApplication1.Aop
{
    public class HandleError : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext Context)
        {
            base.OnException(Context);
            Exception e = Context.Exception;
            SeriLogTool.Error(e.Message);
            SeriLogTool.Error(e.StackTrace);
        }
    }
}