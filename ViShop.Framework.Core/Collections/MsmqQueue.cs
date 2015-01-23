using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ViShop.Framework.Core.Collections
{
    /// <summary>
    /// Msmq队列
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class MsmqQueue<T> : IDisposable, IQueue<T>
    {
        private string prefix = ".\\private$";
        private MessageQueue queue;

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.queue.GetAllMessages().Length;
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix
        {
            get
            {
                return this.prefix;
            }
            set
            {
                this.prefix = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        public MsmqQueue(string name)
        {
            string fullname = string.Format("{0}\\{1}", this.Prefix, name);
            if (!MessageQueue.Exists(fullname))
            {
                this.queue = MessageQueue.Create(fullname);
            }
            else
            {
                this.queue = new MessageQueue(fullname);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            this.queue.Purge();
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns>结果</returns>
        private T Convert(Message message)
        {
            T result = default(T);
            message.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
            result = (T)message.Body;

            return result;
        }

        /// <summary>
        /// 出列
        /// </summary>
        /// <returns>值</returns>
        public T Dequeue()
        {
            return this.Convert(this.queue.Receive());
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this.queue != null)
            {
                this.queue.Dispose();
            }
        }
        /// <summary>
        /// 入列
        /// </summary>
        /// <param name="value">值</param>
        public void Enqueue(T value)
        {
            Message message = new Message();
            message.Body = value;
            message.Formatter = new XmlMessageFormatter(new[] { typeof(T) });
            this.queue.Send(message);
        }
        /// <summary>
        /// 查看
        /// </summary>
        /// <returns>值</returns>
        public T Peek()
        {
            return this.Convert(this.queue.Peek());
        }
    }
}
