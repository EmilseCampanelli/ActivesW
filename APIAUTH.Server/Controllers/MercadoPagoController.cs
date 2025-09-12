using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using MercadoPago.Resource.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace APIAUTH.Server.Controllers
{
    [ApiController]
    [Route("api/mercadopago")]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IRepository<Orden> _repository;
        private readonly IPaymentService _paymentService;

        public MercadoPagoController(IRepository<Orden> repository, IPaymentService paymentService)
        {
            _repository = repository;
            _paymentService = paymentService;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook([FromQuery] string id, [FromQuery] string topic)
        {
            if (topic == "payment")
            {
                var client = new MercadoPago.Client.Payment.PaymentClient();
                var payment = await client.GetAsync(long.Parse(id));

                if (payment.Status == "approved")
                {
                    var orden = _repository.GetFiltered(u => u.MercadoPagoPreferenceId == payment.Order.Id.ToString()).FirstOrDefault();

                    if (orden != null)
                    {
                        orden.MercadoPagoPaymentId = payment.Id;

                        if (payment.Status == "approved")
                            orden.OrdenState = Domain.Enums.OrdenState.Pagado;
                        else if (payment.Status == "rejected")
                            orden.OrdenState = Domain.Enums.OrdenState.PagoRechazado;
                        else
                            orden.OrdenState = Domain.Enums.OrdenState.Pendiente;

                        await _repository.Update(orden);
                    }

                }
            }

            return Ok();
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(int orderId)
        {
            var order = await _repository.Get(orderId);
           

            return Ok(await _paymentService.CreatePaymentPreferenceAsync(order));
        }
    }
}
