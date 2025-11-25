using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Promotions.Delete
{
    public class DeletePromotionHandler : IRequestHandler<DeletePromotionCommand, Unit>
    {
        private readonly IRepository<Promotion> _repo;

        public DeletePromotionHandler(IRepository<Promotion> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var promo = await _repo.Get(request.Id);

            if (promo == null)
                throw new Exception("Promoción no encontrada.");

            promo.Status = BaseState.Eliminado;
            promo.Active = false;
            promo.UpdatedDate = DateTime.UtcNow;

            await _repo.Update(promo);
            return Unit.Value;
        }
    }
}
