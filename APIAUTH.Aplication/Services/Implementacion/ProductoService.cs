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
    public class ProductoService : IProductoService
    {
        private readonly IRepository<Producto> _repository;
        private readonly IMapper _mapper;

        public ProductoService(IRepository<Producto> repository, IMapper mapper)
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

        public async Task<ProductoDto> Get(int id)
        {
            var model = await _repository.Get(id);
            return _mapper.Map<ProductoDto>(model);
        }

        public async Task<List<ProductoDto>> GetAll()
        {
            var productos = await _repository.GetAll();
            return _mapper.Map<List<ProductoDto>>(productos);
        }

        public async Task Inactivate(int id)
        {
            var producto = await _repository.Get(id);
            producto.EstadoProducto = Domain.Enums.EstadoProducto.Eliminado;
            BaseEntityHelper.SetInactive(producto);
            await _repository.Update(producto);
        }

        public async Task<ProductoDto> Save(ProductoDto dto)
        {
            var producto = new Producto();

            if (dto.Id.Equals(0))
            {
                var nuevoProducto = _mapper.Map<Producto>(dto);
                nuevoProducto.EstadoProducto = dto.Stock.Equals(0) ? Domain.Enums.EstadoProducto.SinStock : Domain.Enums.EstadoProducto.Disponible;
                BaseEntityHelper.SetCreated(nuevoProducto);
                producto = await _repository.Add(nuevoProducto);
            }
            else
            {
                var actuProducto = _mapper.Map<Producto>(dto);
                actuProducto.EstadoProducto = dto.Stock.Equals(0) ? Domain.Enums.EstadoProducto.SinStock : Domain.Enums.EstadoProducto.Disponible;
                BaseEntityHelper.SetUpdated(actuProducto);
                producto = await _repository.Update(actuProducto);
            }
            return _mapper.Map<ProductoDto>(producto);

        }

        public async Task<ProductoDto> AddStock(int productoId, int stock)
        {
            var producto = await _repository.Get(productoId);
            producto.Stock += stock;
            producto.EstadoProducto = Domain.Enums.EstadoProducto.Disponible;
            await _repository.Update(producto);

            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task<ProductoDto> RemoveStock(int productoId, int stock)
        {
            var producto = await _repository.Get(productoId);
            producto.Stock -= stock;
            if (producto.Stock < 1)
            {
                producto.EstadoProducto = Domain.Enums.EstadoProducto.SinStock;
            }
            await _repository.Update(producto);

            return _mapper.Map<ProductoDto>(producto);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, ProductoDto dto)
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
