using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class CustomerSegment : BaseEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;

    }
}
