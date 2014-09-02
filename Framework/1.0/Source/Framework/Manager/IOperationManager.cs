using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial interface IOperationManager
    {
        /// <summary>
        /// 获取操作
        /// </summary>
        /// <param name="code">操作编码</param>
        /// <returns>返回操作</returns>
        IOperation LoadByCode(string code);
    }
}
