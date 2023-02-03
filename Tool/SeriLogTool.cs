using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Tool
{
    public class SeriLogTool
    {
        public static bool IsLog = !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["ISLogs"]) ? bool.Parse(ConfigurationManager.AppSettings["ISLogs"]) : false;
        public static string LogFilePath(string LogEvent) => $@"{AppContext.BaseDirectory}Logs\{LogEvent}\log.log";
        public static string SerilogOutputTemplate = "{NewLine}{NewLine}日期：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志级别：{Level}{NewLine}信息：{Message}{NewLine}{Exception}" + new string('-', 50);

        public static Logger logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .MinimumLevel.Debug() // 所有Sink的最小记录级别
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.File(LogFilePath("Debug"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.File(LogFilePath("Information"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.File(LogFilePath("Warning"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.File(LogFilePath("Error"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.File(LogFilePath("Fatal"), rollingInterval: RollingInterval.Day, outputTemplate: SerilogOutputTemplate))
               .CreateLogger();

        public static void Debug(string message)
        {
            if (IsLog) logger.Debug(message);
        }
        public static void Information(string message)
        {
            if (IsLog) logger.Information(message);
        }
        public static void Warning(string message)
        {
            if (IsLog) logger.Warning(message);
        }
        public static void Error(string message)
        {
            if (IsLog) logger.Error(message);
        }
        public static void Fatal(string message)
        {
            if (IsLog) logger.Fatal(message);
        }



    }


}