using APIAUTH.Aplication.CQRS.Commands.Promotions.Create;
using APIAUTH.Aplication.CQRS.Commands.Promotions.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IPromotionService
    {
        Task<long> CreatePromotionAsync(CreatePromotionCommand command);
        Task UpdatePromotionAsync(UpdatePromotionCommand command);
    }
}
