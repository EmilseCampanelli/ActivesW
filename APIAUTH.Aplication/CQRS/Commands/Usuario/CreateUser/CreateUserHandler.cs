using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UsuarioDto>(request);
            var result = await _usuarioService.Save(dto);
            return result.Id;
        }
    }
}
