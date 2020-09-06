using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Extension
{
    public static class JsonUtil
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="source"></param>
        /// <param name="paramaterType"></param>
        /// <returns></returns>
        public static object ToDeserialized(this string source, Type paramaterType)
        {
            return JsonConvert.DeserializeObject(source, paramaterType);
        }
        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return obj == null ? "" : JsonConvert.SerializeObject(obj);
        }
    }
}
