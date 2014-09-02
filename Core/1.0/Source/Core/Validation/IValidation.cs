using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 实体验证
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// 实体验证
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="errorMessages">错误消息</param>
        /// <returns>返回验证结果</returns>
        bool Validate(object entity, out string errorMessages);
    }
}
