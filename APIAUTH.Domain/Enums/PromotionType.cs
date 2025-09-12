using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum PromotionType
    {
        General = 1,        // Applies by business rule (e.g., wholesale)
        TimeBound = 2,      // With validity window (e.g., Cyber Monday)
        ProductSpecific = 3 // Explicit product/category lists
    }
}
