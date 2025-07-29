using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum Gender
    {
        [Display(Name = "Femenino")]
        Femenino,
        [Display(Name = "Masculino")]
        Masculino
    }
}
