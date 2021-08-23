using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

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