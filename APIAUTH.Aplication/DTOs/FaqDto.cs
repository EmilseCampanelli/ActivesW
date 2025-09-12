using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class FaqDto : BaseEntityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
