using APIAUTH.Aplication.CQRS.Commands.Producto.ProductCart.Create;
using APIAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Interfaces
{
    public interface IProductCartService
    {
        void AddProductCart(CreateProductCartCommand command);

    }
}
