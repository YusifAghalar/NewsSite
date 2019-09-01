using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSiteVersion2.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object obj)
        {
            string objectstring = JsonConvert.SerializeObject(obj,Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            session.SetString(key, objectstring);

        }
        public static T GetObject<T>(this ISession session , string key) where T : class
        {
            string stringObj = session.GetString(key);
            if (String.IsNullOrEmpty(stringObj))
                return null;
            T obj = JsonConvert.DeserializeObject<T>(stringObj);
            return obj;
        }

    }
}
