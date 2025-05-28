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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Address> _addressRepository;

        public UserService(IRepository<User> repository, IMapper mapper, IAccountService accountService, IRepository<Role> roleRepository, IRepository<Address> addressRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
            _roleRepository = roleRepository;
            _addressRepository = addressRepository;
        }

        public async Task Activate(int id)
        {
            var usuario = await _repository.Get(id);
            BaseEntityHelper.SetActive(usuario);
            var usuarioDto = _mapper.Map<UserDto>(usuario);
            var userDto = _accountService.ActivePassword(usuarioDto).Result;

            usuario.Account.Password = userDto.Password;
            usuario.Account.PasswordDate = userDto.PasswordDate;
            usuario.Account.IsGenericPassword = userDto.IsGenericPassword;

            await _repository.Update(usuario);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.Get(id) != null;
        }

        public async Task<UserDto> Get(int id)
        {
            var model = await _repository.Get(id);
            return _mapper.Map<UserDto>(model);
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

        public async Task<UserDto> Save(UserDto dto)
        {
            User usuario = new User();

            if (dto.Id.Equals(0))
            {
                dto.Account = await _accountService.Save(dto);
                var nuevoUsuario = _mapper.Map<User>(dto);
                nuevoUsuario.RoleId = 1;
                BaseEntityHelper.SetCreated(nuevoUsuario);
                usuario = await _repository.Add(nuevoUsuario);
            }
            else
            {
                var currentUser = await _repository.Get(dto.Id);
                currentUser.Name = dto.Name;
                currentUser.LastName = dto.LastName;
                currentUser.Document = dto.Document;
                currentUser.TypeDocument = dto.DocumentType;
                currentUser.Phone = dto.Phone;
                currentUser.Email = dto.Email;
                currentUser.BackupEmail = dto.BackupEmail;
                currentUser.Gender = dto.Gender;
                currentUser.CompanyId = dto.CompanyId;
                currentUser.RoleId = dto.RoleId;
                BaseEntityHelper.SetUpdated(currentUser);
                usuario = await _repository.Update(currentUser);
            }
            return _mapper.Map<UserDto>(usuario);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, UserDto dto)
        {
            var validations = new List<(bool isValid, string message)>();

            //TODO: Agregar las validaciones

            //var validator = new CollaboratorValidator();
            //var result = await validator.ValidateAsync(dto);
            //validations.Add((result.IsValid, string.Join(Environment.NewLine, result.Errors.Select(x => $"Campo {x.PropertyName} invalido. Error: {x.ErrorMessage}"))));

            return (isValid: validations.All(x => x.isValid),
                   message: string.Join(Environment.NewLine, validations.Where(x => !x.isValid).Select(x => x.message)));
        }

        public async Task<List<UserDto>> GetAll()
        {
            var collaboratorDto = new List<UserDto>();
            var collaborators = await _repository.GetAll();

            foreach (var collaborator in collaborators)
            {
                collaboratorDto.Add(_mapper.Map<UserDto>(collaborator));
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


    }
}
