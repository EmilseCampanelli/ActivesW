using APIAUTH.Aplication.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Commands.Promotion.Update
{
    public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, Unit>
    {
        private readonly IPromotionService _promotionService;

        public UpdatePromotionCommandHandler(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<Unit> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            await _promotionService.UpdatePromotionAsync(request);
            return Unit.Value;
        }
    }
}
