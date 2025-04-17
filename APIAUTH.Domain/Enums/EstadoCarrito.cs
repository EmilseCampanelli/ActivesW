using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum EstadoCarrito
    {
        [Display(Name = "Activo")]
        Activo,

        [Display(Name = "Comprado")]
        Comprado,

        [Display(Name = "Cancelado")]
        Cancelado
    }
}
