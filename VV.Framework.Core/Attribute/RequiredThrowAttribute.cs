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
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredThrowAttribute : Attribute
    {
        public RequiredThrowAttribute()
        {

        }


        public override bool Match(object obj)
        {
            return base.Match(obj);
        }


        public override bool IsDefaultAttribute()
        {
            return base.IsDefaultAttribute();
        }
    }
}
