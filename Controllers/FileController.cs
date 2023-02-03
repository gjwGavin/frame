using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using WebApplication1._Service._File;
using WebApplication1.Aop;
using WebApplication1.Common.Base;

namespace WebApplication1.Controllers
{
    //兼容Element-UI
    [Intercept]
    public class FileController : Controller
    {
        [HttpPost]
        public string Upload(string CUT_ID, string Code)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new FileServer().Upload(CUT_ID, Code));
        }
        [HttpPost]
        public string GetFileList(string CUT_ID, string Code)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new FileServer().GetFileList(CUT_ID, Code));
        }
        [HttpPost]
        public string FileDelete(string ID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new FileServer().FileDelete(ID));
        }
        [HttpPost]
        public FileStreamResult download(string Url, string Name)
        {
            if (string.IsNullOrWhiteSpace(Url) || string.IsNullOrWhiteSpace(Name)) throw new Exception("文件名为空！");
            //string name = url.Substring(url.LastIndexOf(@"\") + 1).Split(".".ToArray())[0];
            var stream = new FileStream(Url, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return File(stream, "application/octet-stream", Name);
        }

    }
}
