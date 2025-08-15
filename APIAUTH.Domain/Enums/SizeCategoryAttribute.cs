using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SizeCategoryAttribute : Attribute
    {
        public string Category { get; }

        public SizeCategoryAttribute(string category)
        {
            Category = category;
        }
    }
}
