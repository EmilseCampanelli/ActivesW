using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Order;
using MercadoPago.Resource.Preference;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public PaymentService(IRepository<Orden> repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
            MercadoPagoConfig.AccessToken = _config["MercadoPago:AccessToken"];
        }

        public async Task<string> CreatePaymentPreferenceAsync(Orden order, decimal shippingCost)
        {
            var items = order.ProductLine.Select(p => new PreferenceItemRequest
            {
                Id = p.OrdenId.ToString(),
                Description = p.Product.Description,
                PictureUrl = p.Product.ProductImages.FirstOrDefault().Url,
                CategoryId = p.Product.CategoryId.ToString(),
                CurrencyId = "ARS",
                Title = p.Product.Title,
                Quantity = p.Amount,
                UnitPrice = (decimal?)p.Price
            }).ToList();

            if (shippingCost > 0)
            {
                items.Add(new PreferenceItemRequest
                {
                    Title = "Envío Andreani",
                    Quantity = 1,
                    UnitPrice = shippingCost
                });
            }

            var request = new PreferenceRequest
            {
                Items = items,

                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = _config["MercadoPago:SuccessUrl"],
                    Failure = _config["MercadoPago:FailureUrl"],
                    Pending = _config["MercadoPago:PendingUrl"]
                },
                AutoReturn = "approved",

                NotificationUrl = _config["MercadoPago:NotificationUrl"]
            };

            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(request);

            order.MercadoPagoPreferenceId = preference.Id;
            order.OrdenState = Domain.Enums.OrdenState.Pendiente;
            BaseEntityHelper.SetUpdated(order);

            await _repository.Update(order);

            return preference.Id;
        }
    }
}
