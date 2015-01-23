using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViShop.Framework.Caching
{
    public abstract class BaseCache : ICache
    {
        private TimeSpan maxDuration = TimeSpan.FromDays(15);

        /// <summary>
        /// 最大持续时间
        /// </summary>
        public TimeSpan MaxDuration
        {
            get
            {
                return this.maxDuration;
            }
            set
            {
                this.maxDuration = value;
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix
        {
            get;
            set;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Add<T>(string key, T value)
        {
            return this.Add(key, value, maxDuration);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="duration">持续时间</param>
        /// <returns>结果</returns>
        public abstract bool Add<T>(string key, T value, TimeSpan duration);

        /// <summary>
        /// 清理
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public abstract T Get<T>(string key);

        /// <summary>
        /// 获取全名
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>名字</returns>
        public virtual string GetFullName(string key)
        {
            string result = key;
            if (!string.IsNullOrWhiteSpace(this.Prefix))
            {
                result = string.Format("{0}.{1}", this.Prefix, key);
            }
            return result;
        }

        /// <summary>
        /// 多线程获取
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <returns>值集合</returns>
        public abstract IDictionary<string, object> MultiGet(IList<string> keys);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key">键</param>
        public abstract void Remove(string key);

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public bool Set<T>(string key, T value)
        {
            return this.Set<T>(key, value, maxDuration);
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="duration">持续时间</param>
        /// <returns>结果</returns>
        public abstract bool Set<T>(string key, T value, TimeSpan duration);
    }
}
