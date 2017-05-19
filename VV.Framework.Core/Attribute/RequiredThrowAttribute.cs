using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class RequiredThrowAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            switch (value)
            {
                case string str when str.IsNullOrWhiteSpace():
                    throw new ArgumentNullException(nameof(validationContext.MemberName), $"字段：{validationContext.MemberName} 是必须的。");
                case object obj when obj == null:
                    throw new ArgumentNullException(nameof(validationContext.MemberName), $"字段：{validationContext.MemberName} 是必须的。");
            }

            return base.IsValid(value, validationContext);
        }
    }
}
