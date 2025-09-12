using APIAUTH.Domain.Enums;

namespace APIAUTH.Domain.Entities
{
    public class Orden : BaseEntity
    {
        public int UserId { get; set; }
        public OrdenState OrdenState { get; set; }
        public DateTime OrdenDate {  get; set; }

        public virtual User User { get; set; }
        public List<ProductLine> ProductLine { get; set; }

        public string MercadoPagoPreferenceId { get; set; }   // Al crear la preferencia
        public long? MercadoPagoPaymentId { get; set; }       // Al confirmar en webhook
    }
}
