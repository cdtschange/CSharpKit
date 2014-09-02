using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;

namespace Cdts.Core
{
    /// <summary>
    /// Manager类的基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class Manager<TEntity, TPKeyType> : IManager<TEntity, TPKeyType>
        where TEntity : IPKey<TPKeyType>
        where TPKeyType : struct, IComparable, IComparable<TPKeyType>, IEquatable<TPKeyType>
    {
        /// <summary>
        /// Manager构造函数
        /// </summary>
        public Manager()
        {
        }
        /// <summary>
        /// 实体验证器
        /// </summary>
        internal protected abstract IValidation Validator { get; }
        /// <summary>
        /// 数据上下文
        /// </summary>
        internal protected abstract IDataContext<TPKeyType> DataContext { get; }
        internal protected virtual string Root
        {
            get
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                if (path.ToLower() != System.Environment.CurrentDirectory.ToLower())
                {
                    path = AppDomain.CurrentDomain.RelativeSearchPath;
                    path = path.Substring(0, path.Length - 3);
                }
                return path;
            }
        }
        /// <summary>
        /// 事务
        /// </summary>
        internal protected abstract IDataTransaction Transaction { get; }
        /// <summary>
        /// 实体验证
        /// </summary>
        /// <param name="entity">需要验证的实体</param>
        internal protected virtual void ValidateEntity(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", Resources.Resource("EntityIsEmpty", EntityName));
            }
            if (Validator != null)
            {
                string errors;
                if (!Validator.Validate(entity, out errors))
                {
                    throw new ArgumentException(errors);
                }
            }
        }




        public abstract TEntity NewEntity();

        public virtual IManager<TEntity, TPKeyType> GetManager(object condition)
        {
            return this;
        }

        /// <summary>
        /// 编码是否唯一
        /// </summary>
        protected virtual bool IsUniqueCode
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 逻辑删除的实体编码是否保持唯一
        /// </summary>
        protected virtual bool IsUniqueCodeWithLogicalDelete
        {
            get
            {
                return false;
            }
        }

        #region Validate

        /// <summary>
        /// 当创建实体时对实体进行验证
        /// </summary>
        /// <param name="entity">要创建的实体</param>
        internal protected virtual void ValidateEntityForCreate(TEntity entity)
        {
            if (IsIdExisted(entity.Id))
            {
                throw new InvalidOperationException(Resources.Resource("EntityIsExisted", entity.Id, EntityName));
            }
        }
        /// <summary>
        /// 当更新实体是对实体进行验证
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        internal protected virtual void ValidateEntityForUpdate(TEntity entity)
        {
            if (!IsIdExisted(entity.Id))
            {
                throw new InvalidOperationException(Resources.Resource("EntityIsNotExisted", entity.Id, EntityName));
            }
        }
        /// <summary>
        /// 当删除实体时对实体进行验证
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        internal protected virtual void ValidateEntityForDelete(TEntity entity)
        {
            if (!IsIdExisted(entity.Id))
            {
                throw new InvalidOperationException(Resources.Resource("EntityIsNotExisted", entity.Id, EntityName));
            }
        }

        #endregion

        #region Tree

        /// <summary>
        /// 验证树结构的实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isCreate">true为创建时验证，false为更新时验证</param>
        internal protected virtual void ValidateEntityForTree(TEntity entity, bool isCreate)
        {
            if (!IsUniqueCode)
            {
                return;
            }
            if (entity == null)
            {
                throw new ArgumentNullException("entity", Resources.Resource("EntityIsEmpty", EntityName));
            }
            ITree<TEntity> tree = entity as ITree<TEntity>;
            if (tree == null)
            {
                return;
            }
            if (!isCreate)
            {
                TEntity old = DataContext.LoadFromDatabase<TEntity>(entity.Id);

                if (old != null && ((ITree<TEntity>)old).Code == tree.Code)
                {
                    return;
                }
            }
            CoreExpression expression = CoreExpression.Equal("Code", tree.Code);
            if (entity is ILogicalDeleted && !IsUniqueCodeWithLogicalDelete)
            {
                expression = CoreExpression.And(expression, CoreExpression.Equal("Invalid", false));
            }
            int totalRecords = 0;
            IQueryable q = Load(expression, null, null, "Id", 1, 1, out totalRecords);
            int count = q.Count();
            if (count > 0)
            {
                throw new InvalidOperationException(Resources.Resource("EntityCodeIsExisted", tree.Code, EntityName));
            }
        }
        /// <summary>
        /// 对树结构实体编码进行调整
        /// </summary>
        /// <param name="entity">树结构实体</param>
        internal protected virtual void AdjustForTree(ITree<TEntity> entity)
        {
            string oldCode = entity.Code;
            if (entity.Parent != null)
            {
                ITree<TEntity> parent = entity.Parent as ITree<TEntity>;
                entity.Code = parent.Code + "." + oldCode.Substring(oldCode.LastIndexOf(".") + 1);
            }
            else
            {
                entity.Code = oldCode.Substring(oldCode.LastIndexOf(".") + 1);
            }
            ITree<TEntity> oldEntity = LoadFromDatabase(((TEntity)entity).Id) as ITree<TEntity>;
            List<TEntity> children = GetAllChildren(oldEntity).ToList();
            int length = oldEntity != null ? oldEntity.Code.Length : 0;
            children.ForEach(c =>
            {
                ITree<TEntity> cc = (ITree<TEntity>)c;
                cc.Code = entity.Code + cc.Code.Substring(length);
                UpdateEntity((TEntity)cc);
            });
        }
        /// <summary>
        /// 获取所有子孙
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回子孙</returns>
        internal protected virtual IList<TEntity> GetAllChildren(ITree<TEntity> tree)
        {
            if (tree == null)
            {
                return new List<TEntity>();
            }
            CoreExpression condition = CoreExpression.Like("Code", tree.Code + ".");
            int record;
            return Load(condition, CoreOrderBy.Create(new CoreOrderBy("Code", true)), null, 1, int.MaxValue, out record);
        }

        #endregion

        #region Ordered

        /// <summary>
        /// 调整实体的排序值
        /// </summary>
        /// <param name="entity">实体</param>
        internal protected virtual void AdjustForOrder(TEntity entity)
        {
            IDataContext<TPKeyType> dataContext = DataContext;
            IOrdered newEntity = entity as IOrdered;
            if (newEntity == null)
            {
                return;
            }
            int maxOrder = -1;
            int minOrder = -1;
            int step = 0;
            IOrdered oldEntity = dataContext.LoadFromDatabase<TEntity>(entity.Id) as IOrdered;
            if (oldEntity == null)
            {
                maxOrder = int.MaxValue;
                minOrder = newEntity.Order;
                step = 1;
            }
            if (oldEntity != null && newEntity.Order != oldEntity.Order)
            {
                maxOrder = Math.Max(newEntity.Order, oldEntity.Order);
                minOrder = Math.Min(newEntity.Order, oldEntity.Order);
                step = newEntity.Order > oldEntity.Order ? -1 : 1;
            }
            if (maxOrder == minOrder)
            {
                return;
            }
            int record;
            CoreExpression expression = CoreExpression.And(
                CoreExpression.LessThanOrEqual("Order", maxOrder),
                CoreExpression.GreaterThanOrEqual("Order", minOrder));
            if (entity is ILogicalDeleted && !IsUniqueCodeWithLogicalDelete)
            {
                expression = CoreExpression.And(expression, CoreExpression.Equal("Invalid", false));
            }
            IList<TEntity> entities = dataContext.Load<TEntity>(expression, new List<CoreOrderBy>(), 1, int.MaxValue, out record);
            foreach (TEntity orderEntity in entities)
            {
                if (!orderEntity.Id.Equals(entity.Id))
                {
                    IOrdered temp = orderEntity as IOrdered;
                    temp.Order = temp.Order + step;
                    dataContext.Update(temp);
                }
            }
        }

        #endregion

        #region Version

        /// <summary>
        /// 更新带有版本的实体
        /// </summary>
        /// <param name="entity">需要更新的实体</param>
        internal protected virtual void UpdateEntityWithVersion(TEntity entity)
        {
            if (!IsChanged(entity))
            {
                return;
            }
            IVersion<TEntity> newEntity = entity as IVersion<TEntity>;
            if (newEntity == null)
            {
                return;
            }
            TEntity oldEntity = DataContext.LoadFromDatabase<TEntity>(entity.Id);
            if (oldEntity == null)
            {
                return;
            }
            TEntity old = CloneEntityForVersionChanged(oldEntity);
            newEntity.Version++;
            if (old != null)
            {
                ((IVersion<TEntity>)old).Original = entity;
                ((IVersion<TEntity>)old).Invalid = true;
                DataContext.Create<TEntity>(old);
            }
        }

        #endregion

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void CreateEntity(TEntity entity)
        {
            DataContext.Create(entity);
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void UpdateEntity(TEntity entity)
        {
            DataContext.Update(entity);
        }
        /// <summary>
        /// 逻辑删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void LogicalDeleteEntity(TEntity entity)
        {
            ILogicalDeleted obj = entity as ILogicalDeleted;
            if (obj != null)
            {
                obj.Invalid = true;
                DataContext.Update(entity);
            }
            else
            {
                PhysicalDeleteEntity(entity);
            }
        }
        /// <summary>
        /// 物理删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void PhysicalDeleteEntity(TEntity entity)
        {
            DataContext.Delete(entity);
        }
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void RestoreEntity(TEntity entity)
        {
            ILogicalDeleted obj = entity as ILogicalDeleted;
            if (obj != null)
            {
                obj.Invalid = false;
                DataContext.Update(entity);
            }
        }
        /// <summary>
        /// 根据传入的实体值，克隆一个新的实体
        /// </summary>
        /// <param name="entity">被克隆的实体</param>
        /// <returns>克隆出来的新实体。</returns>
        protected virtual TEntity CloneEntityForVersionChanged(TEntity entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="page">页数</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="totalRecords">总数</param>
        /// <returns>返回实体</returns>
        protected virtual IList<TEntity> LoadEntities(CoreExpression condition, IList<CoreOrderBy> orderBy, int page, int pageSize, out int totalRecords)
        {
            return DataContext.Load<TEntity>(condition, orderBy, page, pageSize, out totalRecords);
        }
        protected virtual IList<TChildEntity> LoadEntitiesOfType<TChildEntity>(CoreExpression condition, IList<CoreOrderBy> orderBy, int page, int pageSize, out int totalRecords) where TChildEntity : TEntity
        {
            return DataContext.Load<TChildEntity>(condition, orderBy, page, pageSize, out totalRecords);
        }
        /// <summary>
        /// 根据操作码设置条件
        /// </summary>
        /// <param name="condition">已有条件</param>
        /// <param name="operationCodes">操作码</param>
        /// <returns>根据操作码设置后的条件</returns>
        protected virtual CoreExpression ConditionFromOperationCodes(CoreExpression condition, IList<string> operationCodes)
        {
            return condition;
        }

        protected virtual IQueryable LoadEntities(CoreExpression expression, IList<CoreOrderBy> orderBy, string selector, int page, int pageSize, out int totalRecords)
        {
            if (selector.StartsWith("(") && selector.EndsWith(")"))
            {
                selector = selector.Substring(1, selector.Length - 2);
            }
            if (selector.Contains("(")) //ToDo:更准确地判断Selector是否包含有关联属性以及有哪些关联属性
            {
                IList<TEntity> entities = LoadEntities(expression, orderBy, page, pageSize, out totalRecords);
                SelectorParser parser = new SelectorParser(selector);
                return entities.AsQueryable().Select(parser.Parse());
            }
            return DataContext.Load<TEntity>(expression, orderBy, selector, page, pageSize, out totalRecords);
        }
        protected virtual IQueryable LoadEntitiesOfType<TChildEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, string selector, int page, int pageSize, out int totalRecords)
         where TChildEntity : TEntity
        {
            if (selector.StartsWith("(") && selector.EndsWith(")"))
            {
                selector = selector.Substring(1, selector.Length - 2);
            }
            if (selector.Contains("(")) //ToDo:更准确地判断Selector是否包含有关联属性以及有哪些关联属性
            {
                IList<TChildEntity> entities = LoadEntitiesOfType<TChildEntity>(expression, orderBy, page, pageSize, out totalRecords);
                SelectorParser parser = new SelectorParser(selector);
                return entities.AsQueryable().Select(parser.Parse());
            }
            return DataContext.Load<TChildEntity>(expression, orderBy, selector, page, pageSize, out totalRecords);
        }
        /// <summary>
        /// 实体名称
        /// </summary>
        protected abstract string EntityName { get; }

        #region IManager<TEntity,TPrimaryKeyType> 成员

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体</param>
        [CoreTransaction]
        public virtual void Create(TEntity entity)
        {
            ValidateEntity(entity);
            ValidateEntityForCreate(entity);
            if (entity is ITree<TEntity>)
            {
                ValidateEntityForTree(entity, true);
                AdjustForTree(entity as ITree<TEntity>);
            }
            if (entity is IOrdered)
            {
                AdjustForOrder(entity);
            }
            CreateEntity(entity);
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        [CoreTransaction]
        public virtual void Update(TEntity entity)
        {
            ValidateEntity(entity);
            ValidateEntityForUpdate(entity);
            if (entity is ITree<TEntity>)
            {
                ValidateEntityForTree(entity, false);
                AdjustForTree(entity as ITree<TEntity>);
            }
            if (entity is IOrdered)
            {
                AdjustForOrder(entity);
            }
            if (entity is IVersion<TEntity>)
            {
                UpdateEntityWithVersion(entity);
            }
            UpdateEntity(entity);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        [CoreTransaction]
        public void Delete(TEntity entity)
        {
            ValidateEntity(entity);
            ValidateEntityForDelete(entity);
            if (entity is ILogicalDeleted)
            {
                ILogicalDeleted obj = entity as ILogicalDeleted;
                if (!obj.Invalid)
                {
                    LogicalDeleteEntity(entity);
                    return;
                }
            }
            PhysicalDeleteEntity(entity);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体Id</param>
        [CoreTransaction]
        public void Delete(TPKeyType id)
        {
            TEntity entity = LoadById(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }
        /// <summary>
        /// 删除实体列表
        /// </summary>
        /// <param name="entities">实体列表</param>
        [CoreTransaction]
        public void Delete(IList<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Delete(entity);
            }
        }
        /// <summary>
        /// 删除实体列表
        /// </summary>
        /// <param name="ids">实体Id列表</param>
        [CoreTransaction]
        public void Delete(IList<TPKeyType> ids)
        {
            foreach (TPKeyType id in ids)
            {
                Delete(id);
            }
        }
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="entity">实体</param>
        [CoreTransaction]
        public void Restore(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", Resources.Resource("EntityIsEmpty", EntityName));
            }
            if (entity is ILogicalDeleted)
            {
                RestoreEntity(entity);
            }
        }
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="id">实体Id</param>
        [CoreTransaction]
        public void Restore(TPKeyType id)
        {
            TEntity entity = LoadById(id);
            if (entity != null)
            {
                Restore(entity);
            }
        }
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        [CoreTransaction]
        public void Restore(IList<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                Restore(entity);
            }
        }
        /// <summary>
        /// 恢复实体
        /// </summary>
        /// <param name="ids">实体Id列表</param>
        [CoreTransaction]
        public void Restore(IList<TPKeyType> ids)
        {
            foreach (TPKeyType id in ids)
            {
                Restore(id);
            }
        }
        /// <summary>
        /// 保存所有更改
        /// </summary>
        /// <returns>返回受影响的行数</returns>
        public virtual int SaveChanges()
        {
            return DataContext.SaveChanges();
        }
        /// <summary>
        /// 判断实体是否修改过
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>如果被修改过，返回true，否则返回false</returns>
        public virtual bool IsChanged(TEntity entity)
        {
            return DataContext.IsChanged(entity);
        }
        /// <summary>
        /// 放弃修改
        /// </summary>
        /// <param name="entity">需要放弃修改的实体</param>
        public virtual void Unchange(TEntity entity)
        {
            DataContext.Unchange(entity);
        }
        /// <summary>
        /// 通过实体Id获取实体
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>实体,如果实体不存在,返回null</returns>
        public virtual TEntity LoadById(TPKeyType id)
        {
            return DataContext.Load<TEntity>(id);
        }
        /// <summary>
        /// 通过实体Id获取数据库实体版本
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns></returns>
        public virtual TEntity LoadFromDatabase(TPKeyType id)
        {
            return DataContext.LoadFromDatabase<TEntity>(id);
        }
        /// <summary>
        /// 创建查询
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> CreateQuery()
        {
            return DataContext.CreateQuery<TEntity>();
        }
        public virtual IQueryable<TChildEntity> CreateQueryOfType<TChildEntity>() where TChildEntity : TEntity
        {
            return DataContext.CreateQuery<TChildEntity>();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="operationCodes">操作码</param>
        /// <param name="page">页数</param>
        /// <param name="pageSize">每页行数</param>
        /// <param name="totalRecords">总数</param>
        /// <returns>返回实体</returns>
        public virtual IList<TEntity> Load(CoreExpression condition, IList<CoreOrderBy> orderBy, IList<string> operationCodes, int page, int pageSize, out int totalRecords)
        {
            condition = ConditionFromOperationCodes(condition, operationCodes);
            return LoadEntities(condition, orderBy, page, pageSize, out totalRecords);
        }
        public virtual IList<TChildEntity> LoadOfType<TChildEntity>(CoreExpression condition, IList<CoreOrderBy> orderBy, IList<string> operationCodes, int page, int pageSize, out int totalRecords) where TChildEntity : TEntity
        {
            condition = ConditionFromOperationCodes(condition, operationCodes);
            return LoadEntitiesOfType<TChildEntity>(condition, orderBy, page, pageSize, out totalRecords);
        }

        public IQueryable Load(CoreExpression expression, IList<CoreOrderBy> orderBy, IList<string> operationCodes, string selector, int page, int pageSize, out int totalRecords)
        {
            expression = ConditionFromOperationCodes(expression, operationCodes);
            return LoadEntities(expression, orderBy, selector, page, pageSize, out totalRecords);
        }
        public IQueryable LoadOfType<TChildEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, IList<string> operationCodes, string selector, int page, int pageSize, out int totalRecords)
            where TChildEntity : TEntity
        {
            expression = ConditionFromOperationCodes(expression, operationCodes);
            return LoadEntities(expression, orderBy, selector, page, pageSize, out totalRecords);
        }

        /// <summary>
        /// 根据Id判断实体是否存在
        /// </summary>
        /// <param name="id">实体Id</param>
        /// <returns>如果实体存在，返回true，否则返回false。</returns>
        public virtual bool IsIdExisted(TPKeyType id)
        {
            return LoadById(id) != null;
        }

        #endregion

        #region ITransactionable 成员

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>返回<seealso cref="IDataTransaction"/>实例。</returns>
        public virtual IDataTransaction BeginTransaction()
        {
            Transaction.Begin();
            return Transaction;
        }

        #endregion

        public virtual object GetObject(Type type, object id)
        {
            return DataContext.GetObject(type, id);
        }

    }

}
