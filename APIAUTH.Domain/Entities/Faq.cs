using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Faq : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
