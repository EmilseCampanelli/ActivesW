using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Enums
{
    public enum OrdenState
    {

        [Display(Name = "Pendiente de compra")]
        PendienteCompra,

        [Display(Name = "Pendiente de Pago")]
        Pendiente,

        [Display(Name = "Pagado")]
        Pagado,

        [Display(Name = "En viaje")]
        EnViaje,

        [Display(Name = "Entregado")]
        Entregado,

        [Display(Name = "Cancelado")]
        Cancelado,

        [Display(Name = "Pago Rechazado")]
        PagoRechazado

    }
}
