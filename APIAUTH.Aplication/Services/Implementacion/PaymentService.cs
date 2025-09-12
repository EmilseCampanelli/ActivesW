using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Order;
using MercadoPago.Resource.Preference;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Orden> _repository;

        public PaymentService(IRepository<Orden> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreatePaymentPreferenceAsync(Orden order)
        {
            var request = new PreferenceRequest
            {
                Items = order.ProductLine.Select(p => new PreferenceItemRequest
                {
                    Title = p.Product.Title,
                    Quantity = p.Amount,
                    UnitPrice = (decimal?)p.Price
                }).ToList(),
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://activesw-henna.vercel.app/pago-exitoso",
                    Failure = "https://activesw-henna.vercel.app/pago-fallido",
                    Pending = "https://activesw-henna.vercel.app/pago-pendiente"
                },
                AutoReturn = "approved",
                NotificationUrl = "https://activesw-henna.vercel.app/api/mercadopago/webhook"
            };

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            order.MercadoPagoPreferenceId = preference.Id;
            order.OrdenState = Domain.Enums.OrdenState.Pendiente;
            BaseEntityHelper.SetUpdated(order);

            await _repository.Update(order);

            return preference.InitPoint; // URL del checkout
        }
    }
}
