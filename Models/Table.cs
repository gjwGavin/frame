using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [SugarTable("Table_1")]
    public class TableBO
    {
        public string WEN { set; get; }
        public bool PDFSwtich { get; set; }
        public DateTime time { get; set; }
        public int sort { get; set; }
    }
}