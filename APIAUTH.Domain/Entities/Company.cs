using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string CUIT { get; set; }
        public string Address { get; set; }
        public DateTime OperationDate { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
