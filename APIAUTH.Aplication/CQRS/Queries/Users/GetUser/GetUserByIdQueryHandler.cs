using APIAUTH.Aplication.DTOs;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Queries.Users.GetUser
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserGetDto>
    {
        public Task<UserGetDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
