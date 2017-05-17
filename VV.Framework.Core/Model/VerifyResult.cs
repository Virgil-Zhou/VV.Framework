using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV.Framework.Core
{
    /// <summary>
    /// 模型验证结果
    /// </summary>
    public class VerifyResult
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsValid { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public List<ValidationResult> result { get; set; }
    }
}
