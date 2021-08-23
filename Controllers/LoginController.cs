using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public string Login(string userName,string passWord)
        {
            //
            return Guid.NewGuid().ToString();
        }
    }
}