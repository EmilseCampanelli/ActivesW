using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum Sizes
    {
        // Ropa
        [Display(Name = "Extra Small")]
        [SizeCategory("Clothing")]
        XS,

        [Display(Name = "Small")]
        [SizeCategory("Clothing")]
        S,

        [Display(Name = "Medium")]
        [SizeCategory("Clothing")]
        M,

        [Display(Name = "Large")]
        [SizeCategory("Clothing")]
        L,

        [Display(Name = "Extra Large")]
        [SizeCategory("Clothing")]
        XL,

        // Calzado
        [Display(Name = "Size 38")]
        [SizeCategory("Shoes")]
        Shoe38,

        [Display(Name = "Size 39")]
        [SizeCategory("Shoes")]
        Shoe39,

        [Display(Name = "Size 40")]
        [SizeCategory("Shoes")]
        Shoe40,

        // Sombreros
        [Display(Name = "Hat Small")]
        [SizeCategory("Hats")]
        HatS,

        [Display(Name = "Hat Medium")]
        [SizeCategory("Hats")]
        HatM
    }
}
