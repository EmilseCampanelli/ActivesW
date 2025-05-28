using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Activate(int id)
        {
            var producto = await _repository.Get(id);
            BaseEntityHelper.SetActive(producto);
            await _repository.Update(producto);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.Get(id) != null;
        }

        public async Task<ProductDto> Get(int id)
        {
            var model = await _repository.Get(id);
            return _mapper.Map<ProductDto>(model);
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var productos = await _repository.GetAll();
            return _mapper.Map<List<ProductDto>>(productos);
        }

        public async Task Inactivate(int id)
        {
            var producto = await _repository.Get(id);
            producto.ProductState = Domain.Enums.ProductState.Eliminado;
            BaseEntityHelper.SetInactive(producto);
            await _repository.Update(producto);
        }

        public async Task<ProductDto> Save(ProductDto dto)
        {
            var producto = new Product();

            if (dto.Id.Equals(0))
            {
                var nuevoProducto = _mapper.Map<Product>(dto);
                nuevoProducto.ProductState = dto.Stock.Equals(0) ? Domain.Enums.ProductState.SinStock : Domain.Enums.ProductState.Disponible;
                BaseEntityHelper.SetCreated(nuevoProducto);
                producto = await _repository.Add(nuevoProducto);
            }
            else
            {
                var currentProduct = await _repository.Get(dto.Id);
                currentProduct.Title = dto.Title;
                currentProduct.Description = dto.Description;
                currentProduct.Price = dto.Price;
                currentProduct.Stock = dto.Stock;
                currentProduct.CategoryId = dto.CategoryId;
                currentProduct.Sizes = String.Join(',', dto.Sizes);
                currentProduct.Tags = String.Join(',', dto.Tags);
                currentProduct.Gender = dto.Gender;

                currentProduct.ProductState = dto.Stock.Equals(0) ? Domain.Enums.ProductState.SinStock : Domain.Enums.ProductState.Disponible;
                BaseEntityHelper.SetUpdated(currentProduct);
                producto = await _repository.Update(currentProduct);
            }
            return _mapper.Map<ProductDto>(producto);

        }

        public async Task<ProductDto> AddStock(int productoId, int stock)
        {
            var producto = await _repository.Get(productoId);
            producto.Stock += stock;
            producto.ProductState = Domain.Enums.ProductState.Disponible;
            await _repository.Update(producto);

            return _mapper.Map<ProductDto>(producto);
        }

        public async Task<ProductDto> RemoveStock(int productoId, int stock)
        {
            var producto = await _repository.Get(productoId);
            producto.Stock -= stock;
            if (producto.Stock < 1)
            {
                producto.ProductState = Domain.Enums.ProductState.SinStock;
            }
            await _repository.Update(producto);

            return _mapper.Map<ProductDto>(producto);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, ProductDto dto)
        {
            var validations = new List<(bool isValid, string message)>();

            //TODO: Agregar las validaciones

            //var validator = new CollaboratorValidator();
            //var result = await validator.ValidateAsync(dto);
            //validations.Add((result.IsValid, string.Join(Environment.NewLine, result.Errors.Select(x => $"Campo {x.PropertyName} invalido. Error: {x.ErrorMessage}"))));

            return (isValid: validations.All(x => x.isValid),
                   message: string.Join(Environment.NewLine, validations.Where(x => !x.isValid).Select(x => x.message)));
        }
    }
}
