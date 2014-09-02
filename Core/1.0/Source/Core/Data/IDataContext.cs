using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace Cdts.Core
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    /// <typeparam name="TPKeyType">主键类型</typeparam>
    public interface IDataContext<TPKeyType> : IDisposable
       where TPKeyType : struct
    {
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 创建实体
        /// </summary>
        void Create<TEntity>(TEntity obj) where TEntity : IPKey<TPKeyType>;
        /// <summary>
        /// 更新实体
        /// </summary>
        void Update(object obj);
        /// <summary>
        /// 删除实体
        /// </summary>
        void Delete(object obj);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        TEntity Load<TEntity>(TPKeyType id) where TEntity : IPKey<TPKeyType>;
        /// <summary>
        /// 获取实体（从数据库）
        /// </summary>
        /// <param name="id">实体ID</param>
        TEntity LoadFromDatabase<TEntity>(TPKeyType id) where TEntity : IPKey<TPKeyType>;
        /// <summary>
        /// 实体是否改变
        /// </summary>
        bool IsChanged(object obj);
        /// <summary>
        /// 放弃修改
        /// </summary>
        void Unchange<TEntity>(TEntity obj) where TEntity : IPKey<TPKeyType>;
        bool SupportedTSql { get; }
        /// <summary>
        /// 创建查询
        /// </summary>
        IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : IPKey<TPKeyType>;
        /// <summary>
        /// 获取实体
        /// </summary>
        IQueryable Load<TEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, string selector, int page, int pageSize, out int totalRecords) where TEntity : IPKey<TPKeyType>;
        /// <summary>
        /// 获取实体
        /// </summary>
        IList<TEntity> Load<TEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, int page, int pageSize, out int totalRecords) where TEntity : IPKey<TPKeyType>;
        /// <summary>
        /// 获取实体
        /// </summary>
        IList Load(string tsql, int page, int pageSize, out int totalRecords);
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="tranId">事务ID</param>
        void BeginTransaction(Guid tranId);
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="tranId">事务ID</param>
        /// <param name="isolationLevel">事务级别</param>
        void BeginTransaction(Guid tranId, IsolationLevel isolationLevel);
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="tranId">事务ID</param>
        void Commit(Guid tranId);
        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="tranId">事务ID</param>
        void Rollback(Guid tranId);
        /// <summary>
        /// 销毁事务
        /// </summary>
        /// <param name="tranId">事务ID</param>
        void Dispose(Guid tranId);
        
        bool IsProxy(object obj);
        /// <summary>
        /// 获取类元数据
        /// </summary>
        /// <param name="typeName">类名</param>
        TypeMetadata GetMetadata(string typeName);
        /// <summary>
        /// 填充类元数据
        /// </summary>
        /// <param name="metadata">类元数据</param>
        void FillMetadata(TypeMetadata metadata);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="id">对象主键</param>
        object GetObject(Type type, object id);
    }
}
