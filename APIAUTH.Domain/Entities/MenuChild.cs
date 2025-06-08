using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class MenuChild
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }

        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
