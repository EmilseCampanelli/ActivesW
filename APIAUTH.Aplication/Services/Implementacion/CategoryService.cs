using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _productRepository;

        public CategoryService(IRepository<Category> repository, IMapper mapper, IRepository<Product> productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
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

        public async Task<CategoryDto> Get(int id)
        {
            var model = await _repository.Get(id);
            return _mapper.Map<CategoryDto>(model);
        }

        public async Task<List<CategoryDto>> GetAll()
        {
            var categorias = _repository.GetAll();

            return _mapper.Map<List<CategoryDto>>(categorias);
        }

        public async Task Inactivate(int id)
        {
            var categoria = await _repository.Get(id);
            BaseEntityHelper.SetInactive(categoria);
            await _repository.Update(categoria);
        }

        public async Task<CategoryDto> Save(CategoryDto dto)
        {
            var categoria = new Category();

            if (dto.Id.Equals(0))
            {
                var categoriaNueva = _mapper.Map<Category>(dto);
                BaseEntityHelper.SetCreated(categoriaNueva);
                categoria = await _repository.Add(categoriaNueva);
            }
            else
            {
                var updateCategoria = _mapper.Map<Category>(dto);
                BaseEntityHelper.SetUpdated(updateCategoria);
                categoria = await _repository.Update(updateCategoria);
            }

            return _mapper.Map<CategoryDto>(categoria);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, CategoryDto dto)
        {
            var validations = new List<(bool isValid, string message)>();

            //TODO: Agregar las validaciones

            //var validator = new CollaboratorValidator();
            //var result = await validator.ValidateAsync(dto);
            //validations.Add((result.IsValid, string.Join(Environment.NewLine, result.Errors.Select(x => $"Campo {x.PropertyName} invalido. Error: {x.ErrorMessage}"))));

            return (isValid: validations.All(x => x.isValid),
                   message: string.Join(Environment.NewLine, validations.Where(x => !x.isValid).Select(x => x.message)));
        }


        public async Task<string> DeleteCategory(int categoryId)
        {
            var isUsed = _productRepository.GetFiltered(u => u.CategoryId == categoryId && u.Status == Domain.Enums.BaseState.Activo).Any();

            if (isUsed)
            {
                throw new Exception($"La categoria que desea utilizar esta siendo utilizada en productos activos");
            }


            await _repository.Delete( await _repository.Get(categoryId));

            return $"La categoria fue eliminada con exito";
        }
    }
}
