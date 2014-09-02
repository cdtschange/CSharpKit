using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Collections;
using System.Web;

namespace Cdts.Core.Unity
{
    public class PerRequestLifetimeManager : LifetimeManager
    {
        private static readonly object key = typeof(PerRequestLifetimeManager).FullName;
        [ThreadStatic]
        private static IDictionary backingStore;
        protected static IDictionary<PerRequestLifetimeManager, object> Values
        {
            get
            {
                IDictionary<PerRequestLifetimeManager, object> values;
                //如果HttpContext.Current为null，说明是单元测试，这时生命周期是每线程的。
                if (HttpContext.Current == null)
                {
                    if (backingStore == null)
                    {
                        backingStore = new Hashtable();
                    }
                    values = backingStore[key] as IDictionary<PerRequestLifetimeManager, object>;
                    if (values == null)
                    {
                        values = new Dictionary<PerRequestLifetimeManager, object>();
                        backingStore.Add(key, values);
                    }
                    return values;
                }
                values = HttpContext.Current.Items[key] as IDictionary<PerRequestLifetimeManager, object>;
                if (values == null)
                {
                    values = new Dictionary<PerRequestLifetimeManager, object>();
                    HttpContext.Current.Items.Add(key, values);
                }
                return values;
            }
        }
        public override object GetValue()
        {
            object value = null;
            Values.TryGetValue(this, out value);
            return value;
        }

        public override void SetValue(object newValue)
        {
            if (newValue == null)
            {
                RemoveValue();
                return;
            }
            IDictionary<PerRequestLifetimeManager, object> values = Values;
            object value = null;
            if (values.TryGetValue(this, out value))
            {
                if (value != null && ReferenceEquals(value, newValue))
                {
                    return;
                }
                DisposeValue(value);
            }
            values[this] = newValue;
        }
        public override void RemoveValue()
        {
            IDictionary<PerRequestLifetimeManager, object> values = Values;
            object value = null;
            if (values.TryGetValue(this, out value))
            {
                DisposeValue(value);
                values.Remove(this);
            }
        }

        private static void DisposeValue(object value)
        {
            if (value != null)
            {
                IDisposable disposable = value as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
        public static void DisposeValues()
        {
            IDictionary<PerRequestLifetimeManager, object> values = Values;
            if (values != null)
            {
                foreach (KeyValuePair<PerRequestLifetimeManager, object> value in values)
                {
                    DisposeValue(value.Value);
                }
                HttpContext.Current.Items.Remove(key);
            }
        }
    }
}
