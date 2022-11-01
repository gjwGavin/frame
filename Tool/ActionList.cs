using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Tool
{
    public class ActionList
    {
        public static List<KeyValuePair> _list = null;

        /// <summary>
        /// 跳过验证的接口
        /// </summary>
        public static List<KeyValuePair> List
        {
            get
            {
                if (_list == null)
                {
                    _list = new List<KeyValuePair>();
                    _list.Add(new KeyValuePair() { Key = "Login", Value = "Login" });
                }
                return _list;
            }
        }

        /// <summary>
        /// 判断是不是需要跳过的接口
        /// </summary>
        /// <param name="controllName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool IsNotAction(string controllName, string actionName)
        {
            var obj = List.FirstOrDefault(r => r.Key.Equals(controllName) && r.Value.Equals(actionName));
            if (obj != null)
            {
                return true;
            }
            return false;
        }

        public class KeyValuePair
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

    }
}