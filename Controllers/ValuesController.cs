using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Aop;

namespace WebApplication1.Controllers
{
    [Intercept]
    public class ValuesController : Controller
    {
        
        public string Get(string name) {


            return "value";
        }
    }
}
