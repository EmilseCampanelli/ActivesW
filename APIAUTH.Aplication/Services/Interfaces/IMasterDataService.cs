﻿using APIAUTH.Aplication.DTOs;
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
        Task<List<ComboDto>> GetPais();
        Task<List<ComboDto>> GetCategorias();
        Task<List<ComboDto>> GetProvincias();
    }
}
