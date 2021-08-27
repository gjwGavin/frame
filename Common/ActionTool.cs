using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Common.Base;
using Newtonsoft.Json;

namespace WebApplication1.Common
{
    public class ActionTool
    {


        public static string ToParamBesa(string ErrorString) {
            return  JsonConvert.SerializeObject(new ParamBesa() { Successful = false,Message = ErrorString});
        }

    }
}