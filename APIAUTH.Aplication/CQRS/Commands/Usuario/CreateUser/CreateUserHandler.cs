using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, AuthDto>
    {
        private readonly IUserService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public CreateUserHandler(IUserService usuarioService, IMapper mapper, IAuthenticationService authenticationService)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<AuthDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = new UserDto
            {
                Name = request.Name,
                Email = request.Email,
                LastName = request.LastName,
                Account = new AccountDto { Password = request.Password }
            };
            var result = await _usuarioService.Save(dto);

            return await _authenticationService.AuthenticateUserAsync(request.Email, request.Password);
        }
    }
}
