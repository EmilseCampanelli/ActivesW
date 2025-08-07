using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IRepository<Category> _categoriaRepository;
        private readonly IRepository<Country> _paisRepository;
        private readonly IRepository<Province> _provinciaRepository;
        private readonly IRepository<Role> _rolRepository;


        public MasterDataService(IRepository<Role> rolRepository, IRepository<Category> categoriaRepository,
            IRepository<Country> paisRepository, IRepository<Province> provinciaRepository)
        {
            _rolRepository = rolRepository;
            _paisRepository = paisRepository;
            _categoriaRepository = categoriaRepository;
            _provinciaRepository = provinciaRepository;
        }

        public async Task<List<ComboDto>> GetCategorias()
        {
            var categoriasCbx = new List<ComboDto>();

            var categorias =  _categoriaRepository.GetAll();
            foreach (var c in categorias)
            {
                categoriasCbx.Add(new ComboDto(c.Id, c.Description));
            }
            return categoriasCbx;
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

        public async Task<List<ComboDto>> GetPais()
        {
            var paisCbx = new List<ComboDto>();

            var pais =  _paisRepository.GetAll();
            foreach (var c in pais)
            {
                paisCbx.Add(new ComboDto(c.Id, c.Description));
            }
            return paisCbx;
        }

        public async Task<List<ComboDto>> GetProvincias()
        {
            var provinciasCbx = new List<ComboDto>();

            var provincias =  _provinciaRepository.GetAll();
            foreach (var c in provincias)
            {
                provinciasCbx.Add(new ComboDto(c.Id, c.Description));
            }
            return provinciasCbx;
        }

        public async Task<List<ComboDto>> GetRoles()
        {
            var rolesCbx = new List<ComboDto>();

            var roles =  _rolRepository.GetAll();
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
    }
}
