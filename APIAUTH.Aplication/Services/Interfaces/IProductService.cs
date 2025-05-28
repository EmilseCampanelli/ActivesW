using APIAUTH.Aplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductDto>
    {
        /*
        * Listar productos Disponibles
        * Listar productos Disponibles y sin stock
        * Listar productos Eliminadoss
        */
        Task<ProductDto> AddStock(int productoId, int stock);
        Task<ProductDto> RemoveStock(int productoId, int stock);

    }
}
