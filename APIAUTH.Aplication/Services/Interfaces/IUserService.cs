﻿using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
using Microsoft.AspNetCore.Http;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IUserService : IGenericService<UserDto>
    {
        Task<string> PutImage(IFormFile image);
        Task<List<RoleDto>> GetRoles();
        Task Blocked(int id);
    }
}
