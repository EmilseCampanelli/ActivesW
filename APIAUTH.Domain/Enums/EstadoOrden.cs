using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum EstadoOrden
    {
        [Display(Name = "Pendiente")]
        Pendiente,

        [Display(Name = "Pagado")]
        Pagado,

        [Display(Name = "Cancelado")]
        Cancelado
    }
}
