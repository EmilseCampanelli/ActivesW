using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class ColorTheme
    {
        public int Id { get; set; }
        public string Mode { get; set; } = "light"; // o "dark"
        public string Section { get; set; }         // e.g., "primary", "background"
        public string Property { get; set; }        // e.g., "main", "contrastText"
        public string Value { get; set; }
    }

}
