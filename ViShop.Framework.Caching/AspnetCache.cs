using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ViShop.Framework.Caching
{
    public class AspnetCache : BaseCache
    {
        private Cache cache = HttpRuntime.Cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AspnetCache()
            : this("Vishop.Cache")
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prefix">前缀</param>
        public AspnetCache(string prefix)
        {
            this.Prefix = prefix;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="duration">持续时间</param>
        /// <returns>结果</returns>
        public override bool Add<T>(string key, T value, TimeSpan duration)
        {
            bool result = false;
            if (null != value)
            {
                if (duration <= TimeSpan.Zero)
                {
                    duration = MaxDuration;
                }
                return cache.Add(GetFullName(key), value, null, DateTime.Now.Add(duration), Cache.NoSlidingExpiration, CacheItemPriority.Default, null) == null;
            }
            return result;
        }

        /// <summary>
        /// 清除
        /// </summary>
        public override void Clear()
        {
            IList<string> keys = new List<string>();
            IDictionaryEnumerator caches = cache.GetEnumerator();
            while (caches.MoveNext())
            {
                string key = caches.Key.ToString();
                if (key.StartsWith(Prefix))
                {
                    keys.Add(key);
                }
            }
            foreach (string key in keys)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">值</param>
        /// <returns>结果</returns>
        public override T Get<T>(string key)
        {
            T result = default(T);
            object value = cache.Get(GetFullName(key));
            if (value is T)
            {
                result = (T)value;
            }
            return result;
        }

        /// <summary>
        /// 多线程获取
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <returns>值集合</returns>
        public override IDictionary<string, object> MultiGet(IList<string> keys)
        {
            IDictionary<string, object> values = new Dictionary<string, object>();
            foreach (string key in keys)
            {
                values.Add(key, Get<object>(key));
            }
            return values;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">键</param>
        public override void Remove(string key)
        {
            cache.Remove(GetFullName(key));
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="duration">持续时间</param>
        /// <returns>结果</returns>
        public override bool Set<T>(string key, T value, TimeSpan duration)
        {
            bool result = false;
            if (null != value)
            {
                if (duration <= TimeSpan.Zero)
                {
                    duration = MaxDuration;
                }
                cache.Insert(GetFullName(key), value, null, DateTime.Now.Add(duration), Cache.NoSlidingExpiration);
                result = true;
            }
            return result;
        }
    }
}
