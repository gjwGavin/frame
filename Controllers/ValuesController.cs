using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Aop;
using WebApplication1.Common.Base;

namespace WebApplication1.Controllers
{
    [Intercept]
    public class ValuesController : Controller
    {
        
        public GetRes Get(string name) {

            throw new Exception("2222");
            return new GetRes()
            {
                MyProperty = 1,
                Value = "返回值"

            };
        }

        public class GetRes: BesaBO
        {
            public string Value { get; set; }
            public int MyProperty { get; set; }

        }
    }
}
