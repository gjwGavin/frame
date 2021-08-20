using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.DB
{
    public class DBClient
    {
        //连接符字串
        private static string connectionString = null;
        //数据库类型
        private static int dbType = 0;

        private SqlSugarClient GetInstance()
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                //连接符字串
                ConnectionString = "server=127.0.0.1;Password=1234;User ID=sa;database=Text",
                //数据库类型
                DbType = DbType.SqlServer,
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

        


    }
}