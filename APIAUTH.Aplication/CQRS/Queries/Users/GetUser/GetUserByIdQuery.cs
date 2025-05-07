using APIAUTH.Aplication.DTOs;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Queries.Users.GetUser
{
    public class GetUserByIdQuery : IRequest<UserGetDto>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
