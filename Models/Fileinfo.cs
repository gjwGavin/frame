using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    ///<summary>
    ///文件管理
    ///</summary>
    [SugarTable("Fileinfo")]
    public partial class Fileinfo
    {
        public Fileinfo()
        {


        }
        /// <summary>
        /// Desc:唯一编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string ID { get; set; }

        /// <summary>
        /// Desc:客户ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string CUT_ID { get; set; }

        /// <summary>
        /// Desc:模块Code
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Code { get; set; }

        /// <summary>
        /// Desc:文件名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }
        /// <summary>
        /// Desc:文件后缀
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Suffix { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Sort { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime? CREATED_TIME { get; set; }

        /// <summary>
        /// Desc:更新时间
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime? UPDATED_TIME { get; set; }

    }
}