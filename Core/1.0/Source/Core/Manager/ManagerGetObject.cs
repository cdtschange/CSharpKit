using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    public class ManagerGetObject<TEntity, TPKeyType> : IGetObject
        where TEntity : IPKey<TPKeyType>
        where TPKeyType : struct, IComparable, IComparable<TPKeyType>, IEquatable<TPKeyType>
    {
        private IManager<TEntity, TPKeyType> manager;
        public Dictionary<Type, Type> MappingTypes { get; private set; }

        public ManagerGetObject(IManager<TEntity, TPKeyType> manager)
        {
            this.manager = manager;
            TEntity entity = manager.NewEntity();
            MappingTypes = new Dictionary<Type, Type>();
            MappingTypes.Add(typeof(TEntity), entity.GetType());
        }

        public object GetObject(string keyName, object id, Type objectType)
        {
            return manager.GetObject(objectType, id);
        }

        public object CreateObject(Type objectType)
        {
            return Activator.CreateInstance(objectType);
        }
    }
}
