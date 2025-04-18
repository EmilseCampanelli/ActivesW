using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _repository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRepository<Rol> _roleRepository;
        private readonly IRepository<Domicilio> _domicilioRepository;

        public UsuarioService(IRepository<Usuario> repository, IMapper mapper, IUserService userService, IRepository<Rol> roleRepository, IRepository<Domicilio> domicilioRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userService = userService;
            _roleRepository = roleRepository;
            _domicilioRepository = domicilioRepository;
        }

        public async Task Activate(int id)
        {
            var usuario = await _repository.Get(id);
            BaseEntityHelper.SetActive(usuario);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            var userDto = _userService.ActivePassword(usuarioDto).Result;

            usuario.Cuenta.Password = userDto.Password;
            usuario.Cuenta.PasswordDate = userDto.PasswordDate;
            usuario.Cuenta.IsGenericPassword = userDto.IsGenericPassword;

            await _repository.Update(usuario);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.Get(id) != null;
        }

        public async Task<UsuarioDto> Get(int id)
        {
            var model = await _repository.Get(id);
            return _mapper.Map<UsuarioDto>(model);
        }

        public async Task Inactivate(int id)
        {
            var collaborator = await _repository.Get(id);
            BaseEntityHelper.SetInactive(collaborator);
            await _repository.Update(collaborator);
        }

        public async Task Blocked(int id)
        {
            var collaborator = await _repository.Get(id);
            collaborator.State = Domain.Enums.BaseState.Bloquado;
            await _repository.Update(collaborator);
        }

        public async Task<string> PutImage(IFormFile image)
        {
            var pathImage = await SavePicture(image);
            return string.Join(",", pathImage);
        }

        public async Task<string> SavePicture(IFormFile image)
        {
            var stringPath = "";

            if (image == null)
            {
                return stringPath;
            }
            try
            {
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var imageName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var imagePath = Path.Combine(uploadFolder, imageName);

                using (var stream = new FileStream(imagePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    await image.CopyToAsync(stream);
                }

                stringPath = Path.Combine("Images", imageName); //TODO: Modificar esta ruta
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la imagen", ex);
            }

            return stringPath;
        }

        public async Task<UsuarioDto> Save(UsuarioDto dto)
        {
            Usuario usuario = new Usuario();

            if (dto.Id.Equals(0))
            {
                dto.Cuenta = await _userService.Save(dto);
                var nuevoUsuario = _mapper.Map<Usuario>(dto);
                BaseEntityHelper.SetCreated(nuevoUsuario);
                usuario = await _repository.Add(nuevoUsuario);
            }
            else
            {
                var updatedUsuario = _mapper.Map<Usuario>(dto);
                BaseEntityHelper.SetUpdated(updatedUsuario);
                usuario = await _repository.Update(updatedUsuario);
            }
            return _mapper.Map<UsuarioDto>(usuario);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, UsuarioDto dto)
        {
            var validations = new List<(bool isValid, string message)>();

            //TODO: Agregar las validaciones

            //var validator = new CollaboratorValidator();
            //var result = await validator.ValidateAsync(dto);
            //validations.Add((result.IsValid, string.Join(Environment.NewLine, result.Errors.Select(x => $"Campo {x.PropertyName} invalido. Error: {x.ErrorMessage}"))));

            return (isValid: validations.All(x => x.isValid),
                   message: string.Join(Environment.NewLine, validations.Where(x => !x.isValid).Select(x => x.message)));
        }

        public async Task<List<UsuarioDto>> GetAll()
        {
            var collaboratorDto = new List<UsuarioDto>();
            var collaborators = await _repository.GetAll();

            foreach (var collaborator in collaborators)
            {
                collaboratorDto.Add(_mapper.Map<UsuarioDto>(collaborator));
            }

            return collaboratorDto;
        }

        public async Task<List<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAll();
            var roleDto = new List<RoleDto>();

            foreach (var role in roles)
            {
                roleDto.Add(_mapper.Map<RoleDto>(role));
            }

            return roleDto;
        }

        public async Task<int> AddToUsuarioAsync(int usuarioId, CreateDomicilioCommand dto)
        {
            // Validar si ya existe un domicilio igual para el usuario
            var domicilioExistente = _domicilioRepository.GetFiltered(e => e.UsuarioId == usuarioId &&
                                    e.Calle.ToLower() == dto.Calle.ToLower() && e.Numero.ToLower() == dto.Numero.ToLower() &&
                                    e.CodigoPostal.ToLower() == dto.CodigoPostal.ToLower()).FirstOrDefault();
            if (domicilioExistente != null)
            {
                throw new ApplicationException("Ya existe un domicilio con esos datos para este usuario.");
            }

            var domicilio = _mapper.Map<Domicilio>(dto);
            domicilio.UsuarioId = usuarioId;

            var agregado = await _domicilioRepository.Add(domicilio);
            return agregado.Id;
        }
    }
}
