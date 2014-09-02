using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Objects;
using System.Data;
using System.Data.Objects.DataClasses;
using Cdts.Core;
using System.Linq.Dynamic;
using System.Data.Metadata.Edm;

namespace Cdts.Framework.Data
{
    public sealed class Dictionaries
    {
        private static Dictionary<Guid, DbTransaction> dbTransactions = new Dictionary<Guid, DbTransaction>();
        public static Dictionary<Guid, DbTransaction> DbTransactions
        {
            get
            {
                return dbTransactions;
            }
        }
    }
    internal static class Extensions
    {
        public static void AttachUpdated(this ObjectContext context, EntityObject objectDetached)
        {
            if (objectDetached.EntityState == EntityState.Detached)
            {
                object currentEntityInDb = null;
                if (context.TryGetObjectByKey(objectDetached.EntityKey, out currentEntityInDb))
                {
                    context.ApplyCurrentValues(objectDetached.EntityKey.EntitySetName, objectDetached);
                    context.ApplyReferencePropertyChanges((IEntityWithRelationships)objectDetached, (IEntityWithRelationships)currentEntityInDb); //Custom extensor method
                }
                else
                {
                    throw new ObjectNotFoundException();
                }
            }
        }
        public static void ApplyReferencePropertyChanges(this ObjectContext context, IEntityWithRelationships newEntity, IEntityWithRelationships oldEntity)
        {
            foreach (var relatedEnd in oldEntity.RelationshipManager.GetAllRelatedEnds())
            {
                var oldRef = relatedEnd as EntityReference;
                if (oldRef != null)
                {
                    var newRef = newEntity.RelationshipManager.GetRelatedEnd(oldRef.RelationshipName, oldRef.TargetRoleName) as EntityReference;
                    oldRef.EntityKey = newRef.EntityKey;
                }
            }
        }
    }
    [CoreLogging]
    public abstract class EFDataContext<TPKeyType> : IDataContext<TPKeyType>
        where TPKeyType : struct, IComparable, IComparable<TPKeyType>, IEquatable<TPKeyType>
    {
        //private DbTransaction dbTran;
        //private ITransaction tran;
        protected abstract Dictionary<Type, string> EntitySetNames { get; }
        private ObjectContext context = null;
        public EFDataContext(ObjectContext context)
        {
            this.context = context;
        }
        //public EFDataContext()
        //    : this(null)
        //{
        //}

        protected abstract Dictionary<Type, ObjectQuery> ObjectQueries { get; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ObjectContext Context
        {
            get
            {
                if (context != null)
                {
                    return context;
                }
                return null;
                //Func<object> newContext = () => NewContext();
                //return Suucha.Data.Tools.GetDataContext(ContextIdentifyName, newContext) as ObjectContext;
            }
        }
        protected abstract Dictionary<Type, string> Includes { get; }
        #region IDataContext 成员

        protected virtual string GetEntitySetName(Type entityType)
        {
            if (EntitySetNames.ContainsKey(entityType))
            {
                return EntitySetNames[entityType];
            }
            EntityContainer container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, System.Data.Metadata.Edm.DataSpace.CSpace);
            var setName = container.BaseEntitySets.Where(s => s.ElementType.Name == entityType.Name).Select(s => s.ElementType.Name).FirstOrDefault();
            if (string.IsNullOrEmpty(setName))
            {
                EntitySetNames.Add(entityType, setName);
            }
            return setName;
        }

        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public virtual void Create<TEntity>(TEntity obj) where TEntity : IPKey<TPKeyType>
        {
            Context.AddObject(GetEntitySetName(obj.GetType()), obj);
        }

        public virtual void Update(object obj)
        {
            //var context = Context;
            //EntityObject entity = obj as EntityObject;

            //if (entity == null || entity.EntityState != EntityState.Detached)
            //{
            //    return;
            //}
            //if (entity.EntityKey == null)
            //{
            //    entity.EntityKey = context.CreateEntityKey(GetEntitySetName(obj.GetType()), obj);
            //}

            //context.AttachUpdated(entity);
        }

        public virtual void Delete(object obj)
        {
            Context.DeleteObject(obj);
        }

        public virtual IQueryable<TEntity> CreateQuery<TEntity>() where TEntity : IPKey<TPKeyType>
        {
            return CreateQuery<TEntity>(false);
        }

        protected virtual IQueryable<TEntity> CreateQuery<TEntity>(bool include) where TEntity : IPKey<TPKeyType>
        {
            return CreateObjectQuery<TEntity>(include);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Load<TEntity>(TPKeyType id) where TEntity : IPKey<TPKeyType>
        {
            ObjectQuery<TEntity> qt = Context.CreateQuery<TEntity>(GetEntitySetName(typeof(TEntity)));
            qt = Include(qt);
            return qt.Execute(MergeOption.AppendOnly).FirstOrDefault(e => e.Id.Equals(id));
        }
        protected virtual ObjectQuery<TEntity> Include<TEntity>(ObjectQuery<TEntity> query)
        {
            string includes = "";
            if (Includes.ContainsKey(typeof(TEntity)))
            {
                includes = Includes[typeof(TEntity)];
            }
            if (!string.IsNullOrEmpty(includes))
            {
                string[] props = includes.Split(",".ToCharArray());
                for (int i = 0; i < props.Length; i++)
                {
                    query = query.Include(props[i]);
                }
            }
            return query;
        }
        public virtual TEntity LoadFromDatabase<TEntity>(TPKeyType id) where TEntity : IPKey<TPKeyType>
        {
            ObjectQuery<TEntity> qt = Context.CreateQuery<TEntity>(GetEntitySetName(typeof(TEntity)));
            qt = Include(qt);
            return qt.Execute(MergeOption.NoTracking).FirstOrDefault(e => e.Id.Equals(id));
        }

        public virtual bool IsChanged(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual void Unchange<TEntity>(TEntity obj) where TEntity : IPKey<TPKeyType>
        {
            ObjectQuery<TEntity> qt = Context.CreateQuery<TEntity>(GetEntitySetName(typeof(TEntity)));
            qt.Execute(MergeOption.OverwriteChanges).FirstOrDefault(e => e.Id.Equals(obj.Id));
        }

        public virtual bool SupportedTSql
        {
            get { throw new NotImplementedException(); }
        }

        public virtual IQueryable Load<TEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, string selector, int page, int pageSize, out int totalRecords) where TEntity : IPKey<TPKeyType>
        {
            if (orderBy == null || orderBy.Count < 1)
            {
                if (orderBy == null)
                {
                    orderBy = new List<CoreOrderBy>();
                }
                orderBy.Add(new CoreOrderBy("Id", true));
            }
            var query = CreateQuery<TEntity>(false);// Context.CreateQuery<TEntity>(GetEntitySetName(typeof(TEntity)));
            SelectorParser parser = new SelectorParser(selector);
            var q = query.Where(expression);
            totalRecords = q.Count();
            return q.OrderBy(orderBy).Skip((page - 1) * pageSize).Take(pageSize).Select(parser.Parse());
        }

        public virtual IList<TEntity> Load<TEntity>(CoreExpression expression, IList<CoreOrderBy> orderBy, int page, int pageSize, out int totalRecords) where TEntity : IPKey<TPKeyType>
        {
            if (orderBy == null || orderBy.Count < 1)
            {
                if (orderBy == null)
                {
                    orderBy = new List<CoreOrderBy>();
                }
                orderBy.Add(new CoreOrderBy("Id", true));
            }
            var query = CreateQuery<TEntity>(true);// Context.CreateQuery<TEntity>(GetEntitySetName(typeof(TEntity)));
            if (expression != null)
            {
                query = query.Where(expression);
            }
            query = query.OrderBy(orderBy);
            totalRecords = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        protected ObjectQuery<TEntity> CreateObjectQuery<TEntity>(bool include) where TEntity : IPKey<TPKeyType>
        {
            var query = Context.CreateQuery<TEntity>(GetEntitySetName(typeof(TEntity)));
            if (include)
            {
                query = Include(query);
            }
            return query;
        }
        public virtual System.Collections.IList Load(string tsql, int page, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public virtual void BeginTransaction(Guid tranId)
        {
            BeginTransaction(tranId, IsolationLevel.ReadUncommitted);
        }

        public virtual void BeginTransaction(Guid tranId, IsolationLevel isolationLevel)
        {

            if (!Dictionaries.DbTransactions.ContainsKey(tranId))
            {
                ObjectContext context = Context;
                if (context.Connection.State != ConnectionState.Open)
                {

                    context.Connection.Open();
                }
                DbTransaction tran = context.Connection.BeginTransaction(isolationLevel);
                Dictionaries.DbTransactions.Add(tranId, tran);
            }
        }

        public virtual void Commit(Guid tranId)
        {
            DbTransaction tran = GetDbTransactionFromDictionary(tranId);
            if (tran != null)
            {
                tran.Commit();
                Dictionaries.DbTransactions.Remove(tranId);
            }

        }

        public virtual void Rollback(Guid tranId)
        {
            DbTransaction tran = GetDbTransactionFromDictionary(tranId);
            if (tran != null)
            {
                tran.Rollback();
                Dictionaries.DbTransactions.Remove(tranId);
            }
        }

        public virtual void Dispose(Guid tranId)
        {
            ObjectContext context = Context;
            DbTransaction tran = GetDbTransactionFromDictionary(tranId);
            if (tran != null)
            {
                Dictionaries.DbTransactions.Remove(tranId);
                tran.Dispose();
            }
            if (context != null)
            {
                if (context.Connection != null && context.Connection.State != ConnectionState.Closed)
                    context.Connection.Close();
            }
            //Dispose();
        }
        private DbTransaction GetDbTransactionFromDictionary(Guid tranId)
        {
            if (Dictionaries.DbTransactions.ContainsKey(tranId))
            {
                return (DbTransaction)(Dictionaries.DbTransactions[tranId]);
            }
            return null;
        }
        public virtual bool IsProxy(object obj)
        {
            throw new NotImplementedException();
        }

        public virtual TypeMetadata GetMetadata(string typeName)
        {
            throw new NotImplementedException();
        }

        public virtual void FillMetadata(TypeMetadata metadata)
        {
            throw new NotImplementedException();
        }

        #endregion
        public virtual object GetObject(Type type, object id)
        {
            ObjectContext context = Context;
            string setName = context.DefaultContainerName + "." + GetEntitySetName(type);
            EntityKey key = new EntityKey(setName, "Id", id);
            object obj = null;
            context.TryGetObjectByKey(key, out obj); // 通过主键得到对象
            return obj;
        }
        #region IDisposable 成员

        public virtual void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }

}
