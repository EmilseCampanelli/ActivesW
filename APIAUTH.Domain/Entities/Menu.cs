using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<MenuChild> Children { get; set; }
    }
}
