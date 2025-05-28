using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum ProductState
    {
        [Display(Name = "Disponible")]
        Disponible = 1,

        [Display(Name = "Sin Stock")]
        SinStock = 2,

        [Display(Name = "Eliminado")]
        Eliminado = 3
    }
}
