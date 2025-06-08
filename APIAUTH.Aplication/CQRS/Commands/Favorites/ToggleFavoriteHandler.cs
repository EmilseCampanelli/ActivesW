using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Favorites
{
    public class ToggleFavoriteHandler : IRequestHandler<ToggleFavoriteCommand, Unit>
    {
        private readonly IRepository<Favorite> _repo;

        public ToggleFavoriteHandler(IRepository<Favorite> repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(ToggleFavoriteCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repo
                .GetFiltered(f => f.UserId == request.UserId && f.ProductId == request.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existing != null)
            {
                await _repo.Delete(existing);
            }
            else
            {
                var favorite = new Favorite
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId
                };
                await _repo.Add(favorite);
            }

            return Unit.Value;
        }
    }
}
