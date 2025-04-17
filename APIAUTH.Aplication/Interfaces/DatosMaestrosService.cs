using APIAUTH.Aplication.DTOs;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Enums;
using APIAUTH.Domain.Repository;

namespace APIAUTH.Aplication.Interfaces
{
    public class DatosMaestrosService : IDatosMaestrosService
    {
        private readonly IRepository<Categoria> _categoriaRepository;
        private readonly IRepository<Pais> _paisRepository;
        private readonly IRepository<Provincia> _provinciaRepository;
        private readonly IRepository<UsuarioTipo> _usuarioTipoRepository;
        private readonly IRepository<Rol> _rolRepository;


        public DatosMaestrosService(IRepository<Rol> rolRepository, IRepository<Categoria> categoriaRepository,
            IRepository<Pais> paisRepository, IRepository<Provincia> provinciaRepository, IRepository<UsuarioTipo> usuarioTipoRepository)
        {
            _rolRepository = rolRepository;
            _paisRepository = paisRepository;
            _categoriaRepository = categoriaRepository;
            _provinciaRepository = provinciaRepository;
            _usuarioTipoRepository = usuarioTipoRepository;
        }

        public async Task<List<ComboDto>> GetCategorias()
        {
            var categoriasCbx = new List<ComboDto>();

            var categorias = await _categoriaRepository.GetAll();
            foreach (var c in categorias)
            {
                categoriasCbx.Add(new ComboDto(c.Id, c.Descripcion));
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
            var estadosCbx = EnumHelper.ToDtoList<EstadoOrden>();

            return estadosCbx;
        }

        public async Task<List<ComboDto>> GetPais()
        {
            var paisCbx = new List<ComboDto>();

            var pais = await _paisRepository.GetAll();
            foreach (var c in pais)
            {
                paisCbx.Add(new ComboDto(c.Id, c.Descripcion));
            }
            return paisCbx;
        }

        public async Task<List<ComboDto>> GetProvincias()
        {
            var provinciasCbx = new List<ComboDto>();

            var provincias = await _provinciaRepository.GetAll();
            foreach (var c in provincias)
            {
                provinciasCbx.Add(new ComboDto(c.Id, c.Descripcion));
            }
            return provinciasCbx;
        }

        public async Task<List<ComboDto>> GetRoles()
        {
            var rolesCbx = new List<ComboDto>();

            var roles = await _rolRepository.GetAll();
            foreach (var c in roles)
            {
                rolesCbx.Add(new ComboDto(c.Id, c.Descripcion));
            }
            return rolesCbx;
        }

        public List<ComboDto> GetSexo()
        {
            var sexoCbx = EnumHelper.ToDtoList<Sexo>();

            return sexoCbx;
        }

        public List<ComboDto> GetTipoDocumento()
        {
            var tipoDocumentoCbx = EnumHelper.ToDtoList<TipoDocumento>();

            return tipoDocumentoCbx;
        }

        public async Task<List<ComboDto>> GetTipoUsuario()
        {
            var tipoUsuarioCbx = new List<ComboDto>();

            var tipoUsuario = await _usuarioTipoRepository.GetAll();
            foreach (var c in tipoUsuario)
            {
                tipoUsuarioCbx.Add(new ComboDto(c.Id, c.Descripcion));
            }
            return tipoUsuarioCbx;
        }
    }
}
