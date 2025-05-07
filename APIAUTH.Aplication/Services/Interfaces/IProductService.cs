using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductoDto>
    {
        /*
        * Listar productos Disponibles
        * Listar productos Disponibles y sin stock
        * Listar productos Eliminadoss
        */
        Task<ProductoDto> AddStock(int productoId, int stock);
        Task<ProductoDto> RemoveStock(int productoId, int stock);

    }
}
