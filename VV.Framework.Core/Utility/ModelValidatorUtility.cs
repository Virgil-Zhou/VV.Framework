using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 模型验证
    /// </summary>
    public static class ModelValidatorUtility
    {
        public static void Verify(object instance)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(instance, null, null);
            var isValid = Validator.TryValidateObject(instance, validationContext, validationResults, true);

            if (!isValid)
            {

            }
        }
    }
}
