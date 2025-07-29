using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserService _usuarioService;
        private readonly IMapper _mapper;
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public UpdateUserHandler(IUserService usuarioService, IMapper mapper, CloudinaryDotNet.Cloudinary cloudinary)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<UserDto>(request);

            dto.AvatarUrl = await SubirImagenAsync(request.Image, String.Format("UserAvatar-%d.jpg", dto.Id));

            var result = await _usuarioService.Save(dto); // mismo Save de Create


            return result != null;
        }

        private async Task<string> SubirImagenAsync(string base64, string fileName)
        {
            var bytes = Convert.FromBase64String(base64);
            using var ms = new MemoryStream(bytes);

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, ms),
                Folder = "Usuarios"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                return result.SecureUrl.ToString();
            else
                throw new Exception(result.Error?.Message ?? "Error subiendo imagen");
        }
    }
}
