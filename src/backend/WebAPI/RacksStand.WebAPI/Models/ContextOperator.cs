using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace RacksStand.WebAPI
{
    public class ContextOperator
    {
        private ContextOperator()
        {
        }
        private static ContextData Instance
        {
            get
            {
                // implicit “context” that will flow with async code.
                //CallContext is a specialized collection object similar to a Thread Local Storage for method calls 
                //and provides data slots that are unique to each logical thread of execution. 
                //The slots are not shared across call contexts on other logical threads.
                //Objects can be added to the CallContext as it travels down and back up the execution code path, 
                //and examined by various objects along the path.
                var context = CallContext.LogicalGetData(ContextKeys.SESSION_KEY);
                if (context == null || !(context is ContextData))
                {
                    context = new ContextData();
                    CallContext.LogicalSetData(ContextKeys.SESSION_KEY, context);
                }
                return (ContextData)context;
            }
        }

        /// <summary>
        /// Set context data.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            Instance.SetValue(key, value);
        }

        /// <summary>
        /// Get context data.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return Instance.GetValue(key);
        }

        /// <summary>
        /// Remove all current context data.
        /// </summary>
        public static void Clear()
        {
            Instance.ClearValues();
        }
    }
}