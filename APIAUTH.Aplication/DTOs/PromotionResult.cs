using APIAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class PromotionResult
    {
        public decimal FinalPrice { get; set; }
        public List<Promotion> PromotionsUsed { get; set; }
    }
}
