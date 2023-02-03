using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication1.Common.Base;
using WebApplication1.DB;
using WebApplication1.Models;

namespace WebApplication1._Service._File
{
    //兼容Element-UI
    public class FileServer
    {
        public BesaBO Upload(string CUT_ID, string Code)
        {
            if (string.IsNullOrWhiteSpace(CUT_ID) || string.IsNullOrWhiteSpace(Code))
            {
                return new BesaBO() { code = 20001, message = "参数不对！" };
            }
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["file"];
            //获取上载文件名称
            string _FileName = file.FileName;
            //后缀
            string _Ext = _FileName.Substring(_FileName.LastIndexOf("."));
            //新文件名
            string _NewName = Guid.NewGuid().ToString();
            //获取上传路径
            string ConfigPath = ConfigurationManager.AppSettings["UploadFilePath"].ToString();

            System.IO.Directory.CreateDirectory(ConfigPath);
            var paths = System.IO.Path.Combine(ConfigPath, _NewName + _Ext);
            file.SaveAs(paths);
            var db = DBClient.GetInstance();

            db.Insertable<Fileinfo>(new Fileinfo() { ID = _NewName, Code = Code, Suffix = _Ext, CREATED_TIME = DateTime.Now, CUT_ID = CUT_ID, Name = _FileName, UPDATED_TIME = DateTime.Now }).ExecuteCommand();
            return new BesaBO();
        }
        public ReturnValueBesa<List<fileList>> GetFileList(string CUT_ID, string Code)
        {
            if (string.IsNullOrWhiteSpace(CUT_ID) || string.IsNullOrWhiteSpace(Code))
            {
                return new ReturnValueBesa<List<fileList>>() { code = 20001, message = "参数不对！" };
            }
            var db = DBClient.GetInstance();
            var path = ConfigurationManager.AppSettings["UploadFilePath"].ToString();
            var list1 = db.Queryable<Fileinfo>().Where(x => x.CUT_ID == CUT_ID && x.Code == Code).OrderBy(x => x.CREATED_TIME).ToList();

            var list = list1.Select(r => new fileList() { name = r.Name, url = Path.Combine(path, r.ID + r.Suffix) }).ToList();
            if (list1 == null || list1.Count == 0)
            {
                List<fileList> fileLists = new List<fileList>();
            }
            return new ReturnValueBesa<List<fileList>>() { Value = list };

        }




        public BesaBO FileDelete(string ID)
        {

            if (string.IsNullOrWhiteSpace(ID))
            {
                return new BesaBO() { code = 20001, message = "参数不对！" };
            }
            ID = ID.Substring(ID.LastIndexOf(@"\") + 1).Split(".".ToArray())[0];
            var db = DBClient.GetInstance();
            var bo = db.Queryable<Fileinfo>().Where(r => r.ID == ID).First();
            db.Deleteable<Fileinfo>().Where(r => r.ID == ID).ExecuteCommand();
            string path11 = System.IO.Path.Combine(ConfigurationManager.AppSettings["UploadFilePath"].ToString(), ID + bo.Suffix);
            System.IO.File.Delete(path11);
            return new BesaBO();
        }

        public class fileList
        {
            public string name { get; set; }
            public string url { get; set; }

        }

    }
}