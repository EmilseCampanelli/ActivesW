using APIAUTH.Aplication.DTOs;
using APIAUTH.Shared.Parameters;
using APIAUTH.Shared.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Queries.Users
{
    public class GetUsersQuery : IRequest<PagedResponse<UserGetDto>>
    {
        public UserQueryParameters Parameters { get; set; }

        public GetUsersQuery(UserQueryParameters parameters)
        {
            Parameters = parameters;
        }

    }
}
