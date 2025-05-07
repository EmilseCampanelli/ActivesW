using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public AddressService(IRepository<Address> repository, IMapper mapper, IRepository<User> userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<int> AddToUsuarioAsync(int usuarioId, AddressAddDto dto)
        {
            var addresses = _userRepository.Get(usuarioId).Result.Address;
        
            var exist = addresses.Any(d => d.Street.ToLower() == dto.Street.ToLower() &&
                d.Number.ToLower() == dto.Number.ToLower() &&
                d.ZipCode.ToLower() == dto.ZipCode.ToLower()
            );

            if (exist)
                throw new ApplicationException("There is already an address like this for this user.");

            var address = _mapper.Map<Address>(dto);

            var addressOk = await _repository.Add(address);
            return addressOk.Id;
        }

        public async Task DeleteAsync(int addressId)
        {
            var entity = await _repository.Get(addressId);
            if (entity == null)
                throw new KeyNotFoundException("Address not found");

            await _repository.Delete(entity);
        }

        public async Task<IEnumerable<AddressDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var entity = _userRepository.Get(usuarioId).Result.Address;
            var addressDto = _mapper.Map<List<AddressDto>>(entity);

            return addressDto;

        }

        public async Task UpdateAsync(AddressDto dto)
        {
            var address = await _repository.Get(dto.Id);
            if (address == null)
                throw new KeyNotFoundException("Address no encontrado");

            _mapper.Map(dto, address);
            await _repository.Update(address);
        }
    }
}

