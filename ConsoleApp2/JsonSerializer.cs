#region References

using System;
using System.Collections.Generic;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;

#endregion

namespace Core
{
    public static class JsonSerializer
    {

        //private static readonly JavaScriptSerializer Json = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue };

        //public static string SerializeToJson(this Object inst)
        //{
        //    return Json.Serialize(inst);
        //}

        public static string SerializeToJson<T>(this IList<T> inst)
        {
            return JsonConvert.SerializeObject(inst);
        }

        public static string SerializeToJson<T>(T inst)
        {
            return JsonConvert.SerializeObject(inst);
        }

        public static IEnumerable<T> DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }

        public static T DeserializeObjectFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}