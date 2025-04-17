﻿using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;

namespace APIAUTH.Aplication.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IRepository<Categoria> _repository;
        private readonly IMapper _mapper;

        public CategoriaService(IRepository<Categoria> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Activate(int id)
        {
            var categoria = await _repository.Get(id);
            BaseEntityHelper.SetActive(categoria);
            await _repository.Update(categoria);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.Get(id) != null;
        }

        public async Task<CategoriaDto> Get(int id)
        {
            var model = await _repository.Get(id);
            return _mapper.Map<CategoriaDto>(model);
        }

        public async Task<List<CategoriaDto>> GetAll()
        {
            var categorias = await _repository.GetAll();

            return _mapper.Map<List<CategoriaDto>>(categorias);
        }

        public async Task Inactivate(int id)
        {
            var categoria = await _repository.Get(id);
            BaseEntityHelper.SetInactive(categoria);
            await _repository.Update(categoria);
        }

        public async Task<CategoriaDto> Save(CategoriaDto dto)
        {
            var categoria = new Categoria();

            if (dto.Id.Equals(0))
            {
                var categoriaNueva = _mapper.Map<Categoria>(dto);
                BaseEntityHelper.SetCreated(categoriaNueva);
                categoria = await _repository.Add(categoriaNueva);
            }
            else
            {
                var updateCategoria = _mapper.Map<Categoria>(dto);
                BaseEntityHelper.SetUpdated(updateCategoria);
                categoria = await _repository.Update(updateCategoria);
            }

            return _mapper.Map<CategoriaDto>(categoria);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, CategoriaDto dto)
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
