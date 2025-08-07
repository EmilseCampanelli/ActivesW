using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum BaseState
    {
        [Display(Name = "Activo")]
        Activo = 1,
        [Display(Name = "Inactivo")]
        Inactivo,
        [Display(Name = "Eliminado")]
        Eliminado,
        [Display(Name = "Bloquado")]
        Bloquado
    }
}
