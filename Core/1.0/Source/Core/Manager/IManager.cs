using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// Manager接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    /// <typeparam name="TPKeyType">主键类型</typeparam>
    public interface IManager<TEntity, TPKeyType> : ITransactionable
        where TEntity : IPKey<TPKeyType>
        where TPKeyType : struct, IComparable, IComparable<TPKeyType>, IEquatable<TPKeyType>
    {
        /// <summary>
        /// 创建一个新的实例
        /// </summary>
        /// <returns>实体</returns>
        TEntity NewEntity();
        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Create(TEntity entity);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(TEntity entity);
        /// <summary>
        /// 根据实体Id删除实体
        /// </summary>
        /// <param name="id">实体Id</param>
        void Delete(TPKeyType id);
        /// <summary>
        /// 删除实体列表
        /// </summary>
        /// <param name="entities">实体列表</param>
        void Delete(IList<TEntity> entities);
        /// <summary>
        /// 根据实体Id列表删除实体
        /// </summary>
        /// <param name="ids">实体Id列表</param>
        void Delete(IList<TPKeyType> ids);
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="entity">要恢复的实体</param>
        void Restore(TEntity entity);
        /// <summary>
        /// 根据实体Id恢复实体
        /// </summary>
        /// <param name="id">实体Id</param>
        void Restore(TPKeyType id);
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="entities">要恢复的实体列表</param>
        void Restore(IList<TEntity> entities);
        /// <summary>
        /// 根据实体Id列表恢复实体
        /// </summary>
        /// <param name="ids">实体Id列表</param>
        void Restore(IList<TPKeyType> ids);
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns>受影响的行数</returns>
        int SaveChanges();
        /// <summary>
        /// 判断实体是否改变
        /// </summary>
        /// <param name="entity">需要判断的实体</param>
        /// <returns>如果实体已改变，返回true，否则返回false</returns>
        bool IsChanged(TEntity entity);
        /// <summary>
        /// 取消实体的改变，将实体恢复成数据库中的值。
        /// </summary>
        /// <param name="entity">要取消改变的实体</param>
        void Unchange(TEntity entity);
        /// <summary>
        /// 根据实体Id获取实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>如果实体存在返回实体，否则返回null。</returns>
        TEntity LoadById(TPKeyType id);
        /// <summary>
        /// 根据实体Id获取数据库中的实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>返回数据库中的实体，如果Id不存在返回null</returns>
        TEntity LoadFromDatabase(TPKeyType id);
        /// <summary>
        /// 创建查询
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> CreateQuery();
        IQueryable<TChildEntity> CreateQueryOfType<TChildEntity>() where TChildEntity : TEntity;
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="operationCodes">操作码列表</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalRecords">总记录数</param>
        /// <returns>实体列表。</returns>
        IList<TEntity> Load(CoreExpression condition, IList<CoreOrderBy> orderBy, IList<string> operationCodes, int page, int pageSize, out int totalRecords);
        IList<TChildEntity> LoadOfType<TChildEntity>(CoreExpression condition, IList<CoreOrderBy> orderBy, IList<string> operationCodes, int page, int pageSize, out int totalRecords) where TChildEntity : TEntity;
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="operationCodes">操作码列表</param>
        /// <param name="selector">属性选择符</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalRecords">总记录数</param>
        /// <returns>返回实体列表的IQueryable实例。</returns>
        IQueryable Load(CoreExpression expression, IList<CoreOrderBy> orderBy, IList<string> operationCodes, string selector, int page, int pageSize, out int totalRecords);
        IQueryable LoadOfType<TChildEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, IList<string> operationCodes, string selector, int page, int pageSize, out int totalRecords) where TChildEntity : TEntity;
        /// <summary>
        /// 根据Id判断实体是否存在
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>如果实体存在返回true，否则返回false。</returns>
        bool IsIdExisted(TPKeyType id);
        /// <summary>
        /// 根据Id获取类型为type的实体
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="id">Id</param>
        /// <returns>实体</returns>
        object GetObject(Type type, object id);

        IManager<TEntity, TPKeyType> GetManager(object condition);
    }

}
