using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;

namespace Cdts.Framework
{
    public interface IDataContextFactory<TPKeyType> where TPKeyType : struct
    {
       // IDataContext<TPKeyType> Create();
    }
}
