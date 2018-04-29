using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacksStand.WebAPI
{
    internal class ContextData
    {
        /// <summary>
        /// Store context data.
        /// </summary>
        private readonly ConcurrentDictionary<string, object> _values = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Set context data.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, object value)
        {
            return _values.TryAdd(key, value);
        }
        /// <summary>
        /// Get context data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string key)
        {
            object value;
            _values.TryGetValue(key, out value);
            return value;

        }
        /// <summary>
        /// Removes all context data.
        /// </summary>
        public void ClearValues()
        {
            _values.Clear();
        }
    }
}