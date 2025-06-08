using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using APIAUTH.Shared.Response;
using AutoMapper;
using MediatR;

namespace APIAUTH.Aplication.CQRS.Queries.Users
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResponse<UserGetDto>>
    {
        private readonly IListRepository<User> _repository;
        private readonly IRepository<User> _readOnlyRepo;
        private readonly IMapper _mapper;


        public GetUsersQueryHandler(IListRepository<User> repository, IRepository<User> readOnlyRepo, IMapper mapper)
        {
            _repository = repository;
            _readOnlyRepo = readOnlyRepo;
            _mapper = mapper;
        }


        public async Task<PagedResponse<UserGetDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var query = _readOnlyRepo.GetAll();

            return await _repository.GetPagedResultAsync(
                query,
                request.Parameters,
                p => _mapper.Map<UserGetDto>(p)
            );
        }
    }
}
