using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;

namespace Cdts.Framework
{
    public abstract class FrameworkManager<TEntity> : Manager<TEntity, Guid>
         where TEntity : IPKey<Guid>
    {
        /// <summary>
        /// Framework配置
        /// </summary>
        protected internal virtual IFrameworkSetting FrameworkSetting
        {
            get
            {
                return Configurations.FrameworkSetting;
            }
        }
        /// <summary>
        /// 验证
        /// </summary>
        protected override IValidation Validator
        {
            get { return ValidationFactory.Create("Framework"); }
        }
        /// <summary>
        /// 数据上下文
        /// </summary>
        protected override IDataContext<Guid> DataContext
        {
            get
            {
                return DataContextFactory.Create();
            }
        }
        /// <summary>
        /// 事务
        /// </summary>
        protected override IDataTransaction Transaction
        {
            get { return DataContextFactory.CreateDataTransaction(); }
        }
        /// <summary>
        /// 新实体
        /// </summary>
        /// <returns></returns>
        public override TEntity NewEntity()
        {
            return ModelFactory.Create<TEntity>();
        }
        /// <summary>
        /// 当前用户
        /// </summary>
        public virtual IUser CurrentUser
        {
            get
            {
                return Context.CurrentUser;
            }
            set
            {
                Context.CurrentUser = value;
            }
        }
    }
}
