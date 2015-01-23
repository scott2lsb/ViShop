using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViShop.Framework.Core.Collections
{
    /// <summary>
    /// 队列
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IQueue<T>
    {
        /// <summary>
        /// 数量
        /// </summary>
        int Count
        {
            get;
        }

        /// <summary>
        /// 清除
        /// </summary>
        void Clear();

        /// <summary>
        /// 出列
        /// </summary>
        /// <returns>值</returns>
        T Dequeue();

        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="value">值</param>
        void Enqueue(T value);

        /// <summary>
        /// 查看 
        /// </summary>
        /// <returns>值</returns>
        T Peek();
    }
}
