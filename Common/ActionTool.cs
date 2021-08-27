using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Common.Base;
using Newtonsoft.Json;
using static WebApplication1.Common.Enum.CommEnum;
using System.Reflection;

namespace WebApplication1.Common
{
    public class ActionTool
    {

        /// <summary>
        /// 错误返回方法
        /// </summary>
        /// <param name="ErrorString"></param>
        /// <returns></returns>
        public static string ToParamBesa(string ErrorString) {
            return  JsonConvert.SerializeObject(new ParamBesa() { Successful = false,Message = ErrorString});
        }
        /// <summary>
        /// 根据某一字段对list对象 进行排序
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="enumSort">排序方式</param>
        /// <param name="sortExpression">排序字段（只可为基本类型）</param>
        /// <param name="data">需要排序的集合</param>
        /// <returns></returns>
        public static List<T> OrderBy<T>(EnumSort enumSort, string sortExpression, List<T> data)
        {
            List<T> afterOrderByList = new List<T>();
            var isName = typeof(T).GetProperty(sortExpression);
            if (isName == null) throw new Exception("排序字段不在该对象中！");
            if (enumSort == EnumSort.ASC)
            {
                afterOrderByList = data.OrderBy(r => r.GetType().GetProperty(sortExpression).GetValue(r, null)).ToList();
            }
            else if (enumSort == EnumSort.DESC)
            {
                afterOrderByList = data.OrderByDescending(r => r.GetType().GetProperty(sortExpression).GetValue(r, null)).ToList();
            }
            return afterOrderByList;
        }
        /// <summary>
        /// 对象转字典
        /// </summary>
        /// <typeparam name="T">需要转的对象类型</typeparam>
        /// <param name="t"></param>
        /// <returns>字典对象</returns>
        public static Dictionary<string, object> GetObjectToDic<T>(T t)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
            if (t == null)
            {
                return keyValuePairs;
            }
            var typeList = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            foreach (var item in typeList)
            {
                if (!keyValuePairs.ContainsKey(item.Name)) keyValuePairs.Add(item.Name, null);
                if (item.GetValue(t) != null) keyValuePairs[item.Name] = item.GetValue(t);
            }
            return keyValuePairs;
        }
        /// <summary>
        /// 字典转对象
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="dic">转换的字典</param>
        /// <returns>返回的对象</returns>
        public static T DicToObjectTo<T>(Dictionary<string, string> dic) where T : new()
        {

            T entity = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dic).Replace(@"\", "").Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}"));
            return entity;
        }

    }
}