using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Cdts.Core
{
    /// <summary>
    /// EntLib验证
    /// </summary>
    public class EntLibValidation : IValidation
    {

        IConfigurationSource source;
        public EntLibValidation(string configPath)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configPath);
            source = new FileConfigurationSource(path);
        }

        #region IValidation 成员
        /// <summary>
        /// 对实体进行验证
        /// </summary>
        /// <param name="entity">需要验证的实体</param>
        /// <param name="errorMessages">实体验证失败的错误消息</param>
        /// <returns>返回true表示验证成功，false验证失败。</returns>
        public bool Validate(object entity, out string errorMessages)
        {
            Type targetType = entity.GetType();
            Validator validator = ValidationFactory.CreateValidatorFromConfiguration(targetType, "default", source);
            errorMessages = "";
            ValidationResults results = validator.Validate(entity);
            StringBuilder errorBuilder = new StringBuilder();
            foreach (ValidationResult result in results)
            {
                errorBuilder.Append(Resources.Resource("ValidateErrorMsg", result.Tag, result.Message));
            }
            if (errorBuilder.ToString().Length > 0)
            {
                errorMessages = errorBuilder.ToString();
                return false;
            }
            return true;
        }

        #endregion
    }
}
