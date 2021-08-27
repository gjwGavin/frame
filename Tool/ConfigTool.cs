using System;
using System.Collections.Generic;
using System.Configuration;

namespace WebApplication1.Tool
{
    public class ConfigTool
    {

        private static Dictionary<string, string> _ConfigDict = null;
        //appSettings 配置的字段
        public enum ConfigField
        {
            sqlConnectionString = 0,
            DbType = 1,
            TokenTime = 2,
            TokenField = 3
        }


        public static Dictionary<string, string> ConfigDict
        {
            get
            {
                if (_ConfigDict == null)
                {
                    _ConfigDict = new Dictionary<string, string>();
                    foreach (string item in Enum.GetNames(typeof(ConfigField)))
                    {
                        _ConfigDict.Add(item, ConfigurationManager.AppSettings[item].ToString());
                    }
                }
                return _ConfigDict;
            }
        }

        /// <summary>
        /// 获取webCofig appSettings配置的内容
        /// </summary>
        /// <param name="Field">key字段</param>
        /// <returns></returns>
        public static string GetConfigValue(ConfigField Field)
        {
            if (!ConfigDict.ContainsKey(Field.ToString())) throw new ConfigurationErrorsException();
            return ConfigDict[Field.ToString()];
        }




    }
}