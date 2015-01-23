using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViShop.Domain.Aggregates;

namespace ViShop.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void Save(TEntity entity);
        /// <summary>
        /// 根据契约获取数据
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindBySpecification(Func<TEntity, bool> spec);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>

        void Delete(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
    }
}