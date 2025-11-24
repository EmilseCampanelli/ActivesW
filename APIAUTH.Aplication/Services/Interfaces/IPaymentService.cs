using APIAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentPreferenceAsync(Orden order, decimal costTracking);
    }
}
