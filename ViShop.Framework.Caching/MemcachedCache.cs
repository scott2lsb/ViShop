using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViShop.Framework.Caching
{
    public class MemcachedCache : BaseCache
    {
        private static MemcachedClient memcached = new MemcachedClient();

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
            if (duration <= TimeSpan.Zero)
            {
                duration = this.MaxDuration;
            }

            return memcached.Store(StoreMode.Add, this.GetFullName(key), value, duration);
        }

        /// <summary>
        /// 清除
        /// </summary>
        public override void Clear()
        {
            memcached.FlushAll();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public override T Get<T>(string key)
        {
            return memcached.Get<T>(this.GetFullName(key));
        }

        /// <summary>
        /// 多线程获取
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <returns>值集合</returns>
        public override IDictionary<string, object> MultiGet(IList<string> keys)
        {
            IEnumerable<string> fullKeys = keys.Select<string, string>(k => this.GetFullName(k));

            return memcached.Get(fullKeys);
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public override void Remove(string key)
        {
            memcached.Remove(this.GetFullName(key));
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
            if (duration <= TimeSpan.Zero)
            {
                duration = this.MaxDuration;
            }

            return memcached.Store(StoreMode.Set, this.GetFullName(key), value, duration);
        }
    }
}
