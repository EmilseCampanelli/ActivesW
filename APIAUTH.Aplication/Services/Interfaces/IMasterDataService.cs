using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IMasterDataService
    {
        List<ComboDto> GetSexo();
        List<ComboDto> GetEstadoCarrito();
        List<ComboDto> GetEstadoOrden();
        List<ComboDto> GetTipoDocumento();
        List<ComboDto> GetEstadoProducto();
        Task<List<ComboDto>> GetRoles();
        Task<List<ComboDto>> GetCategorias();
        List<ComboDto> GetEstados();
        Task<List<ComboUbiDto>> GetPaisesAsync();
        Task<List<ComboDto>> GetProvinciasAsync(string countryIso2);
        Task<List<ComboUbiDto>> GetCiudadesAsync(string countryIso2, int? stateId = null, string? q = null, int top = 50);
        List<ComboSizeDto> GetSizeAsync();
        List<ComboDto> GetDiscountType();
        List<ComboDto> GetPromotionType();
    }
}
