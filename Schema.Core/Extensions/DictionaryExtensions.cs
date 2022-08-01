using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static object GetValue(this IDictionary<string, object> dictionary, string key)
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : null;
        }

        public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            object val = null;
            dictionary.TryGetValue(key, out val);
            return val.GetValue<T>();
        }

        public static T GetValue<T>(this object obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            t = u ?? t;

            return (obj == null) ? default(T) : (T)Convert.ChangeType(obj, t);
        }

        public static object GetDbNullableString(string obj)
        {
            if (!string.IsNullOrEmpty(obj)) return obj.Trim();
            else return DBNull.Value;
        }
    }
}
