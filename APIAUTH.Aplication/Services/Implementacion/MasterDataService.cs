using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IRepository<Category> _categoriaRepository;
        private readonly IRepository<Role> _rolRepository;
        private readonly IGeoRepo _geoRepo;


        public MasterDataService(IRepository<Role> rolRepository, IRepository<Category> categoriaRepository, IGeoRepo geoRepo)
        {
            _rolRepository = rolRepository;
            _categoriaRepository = categoriaRepository;
            _geoRepo = geoRepo;
        }

        public async Task<List<ComboDto>> GetCategorias()
        {
            var categoriasCbx = new List<ComboDto>();

            var categorias = _categoriaRepository.GetAll();
            foreach (var c in categorias)
            {
                categoriasCbx.Add(new ComboDto(c.Id, c.Description));
            }
            return categoriasCbx;
        }

        public async Task<List<ComboUbiDto>> GetPaisesAsync()
        {
            var countriesCbx = new List<ComboUbiDto>();

            var countries =await _geoRepo.GetPaisesAsync();
            foreach (var co in countries)
            {
                countriesCbx.Add(new ComboUbiDto(co.Iso2, co.Name));
            }

            return countriesCbx;
        }

        public List<ComboDto> GetEstadoCarrito()
        {
            var estadosCbx = EnumHelper.ToDtoList<EstadoCarrito>();

            return estadosCbx;
        }

        public List<ComboDto> GetEstadoOrden()
        {
            var estadosCbx = EnumHelper.ToDtoList<OrdenState>(OrdenState.PendienteCompra);

            return estadosCbx;
        }

        public List<ComboDto> GetEstadoProducto()
        {
            var estadosCbx = EnumHelper.ToDtoList<ProductState>();

            return estadosCbx;
        }


        public async Task<List<ComboDto>> GetRoles()
        {
            var rolesCbx = new List<ComboDto>();

            var roles = _rolRepository.GetAll();
            foreach (var c in roles)
            {
                rolesCbx.Add(new ComboDto(c.Id, c.Description));
            }
            return rolesCbx;
        }

        public List<ComboDto> GetSexo()
        {
            var sexoCbx = EnumHelper.ToDtoList<Gender>();

            return sexoCbx;
        }

        public List<ComboDto> GetTipoDocumento()
        {
            var tipoDocumentoCbx = EnumHelper.ToDtoList<DocumentType>();

            return tipoDocumentoCbx;
        }

        public List<ComboDto> GetEstados()
        {
            var estadosCbx = EnumHelper.ToDtoList<BaseState>();

            return estadosCbx;
        }

        

        public async Task<List<ComboDto>> GetProvinciasAsync(string countryIso2)
        {
            var stateCbx = new List<ComboDto>();

            var states = await _geoRepo.GetProvinciasAsync(countryIso2);
            foreach (var co in states)
            {
                stateCbx.Add(new ComboDto(co.Id, co.Name));
            }

            return stateCbx;
        }

        public async Task<List<ComboUbiDto>> GetCiudadesAsync(string countryIso2, int? stateId = null, string? q = null, int top = 50)
        {
            var citiesCbx = new List<ComboUbiDto>();

            var cities = await _geoRepo.GetCiudadesAsync(countryIso2,stateId);
            foreach (var co in cities)
            {
                citiesCbx.Add(new ComboUbiDto(co.Id.ToString(), co.Name));
            }

            return citiesCbx;
        }

        public List<ComboSizeDto> GetSizeAsync()
        {
            var estadosCbx = EnumHelper.ToDtoListSize<Sizes>();

            return estadosCbx;
        }
    }
}
