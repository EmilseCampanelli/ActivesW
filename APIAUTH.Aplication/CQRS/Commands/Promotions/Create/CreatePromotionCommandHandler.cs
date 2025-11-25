using APIAUTH.Aplication.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Promotions.Create
{
    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, long>
    {
        private readonly IPromotionService _promotionService;

        public CreatePromotionCommandHandler(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<long> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            return await _promotionService.CreatePromotionAsync(request);
        }
    }
}
