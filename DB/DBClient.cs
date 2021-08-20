using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.DB
{
    public class DBClient
    {
        //连接符字串
        private static string _ConnectionString = null;
        //数据库类型
        private static int? _DbType = null;

        public static SqlSugarClient GetInstance()
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                //连接符字串
                ConnectionString = ConnectionString,
                //数据库类型
                DbType = (DbType)dbType.Value,
                //自动释放和关闭数据库连接，如果有事务事务结束时关闭，否则每次操作后关闭
                IsAutoCloseConnection = true
            });
            db.Ado.CommandTimeOut = 30;
            //添加Sql打印事件，开发中可以删掉这个代码
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);
            };
            return db;
        }

        /// <summary>
        /// 读取数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (_ConnectionString == null)
                {
                    _ConnectionString = ConfigurationManager.AppSettings["sqlConnectionString"].ToString();
                    if (string.IsNullOrEmpty(_ConnectionString))
                    {
                        throw new Exception("数据库连接字符串错误！！！");
                    }
                }
                return _ConnectionString;
            }
        }
        /// <summary>
        /// 读取数据库连接数据类型
        /// </summary>
        public static int? dbType
        {
            get
            {
                if (_DbType == null)
                {
                    int _dbtype;
                    bool isInt = int.TryParse(ConfigurationManager.AppSettings["DbType"].ToString(), out _dbtype);
                    if (!isInt)
                    {
                        throw new Exception("数据库类型错误！！！");
                    }
                    _DbType = _dbtype;
                }
                return _DbType;
            }
        }
    }
}
