using APIAUTH.Aplication.DTOs;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<int> AddToUsuarioAsync(int usuarioId, AddressAddDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(AddressDto dto);
    }
}
