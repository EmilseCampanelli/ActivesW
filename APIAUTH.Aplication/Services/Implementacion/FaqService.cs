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
    public class FaqService : IFaqService
    {
        private readonly IRepository<Faq> _repository;
        private readonly IMapper _mapper;

        public FaqService(IRepository<Faq> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Activate(int id)
        {
            var faq = await _repository.Get(id);
            BaseEntityHelper.SetActive(faq);
            await _repository.Update(faq);
        }

        public async Task<bool> Exists(int id)
        {
            return await _repository.Get(id) != null;
        }

        public async Task<FaqDto> Get(int id, int userId = 0)
        {
            var model = await _repository.Get(id);
            var faqDto = _mapper.Map<FaqDto>(model);
            return faqDto;
        }

        public async Task<List<FaqDto>> GetAll()
        {
            var faqList = _repository.GetAll();
            return _mapper.Map<List<FaqDto>>(faqList);
        }

        public async Task Inactivate(int id)
        {
            var faq = await _repository.Get(id);
            BaseEntityHelper.SetInactive(faq);
            await _repository.Update(faq);
        }

        public async Task<FaqDto> Save(FaqDto dto)
        {
            var faq = new Faq();

            if (dto.Id.Equals(0))
            {
                var newFaq = _mapper.Map<Faq>(dto);
                BaseEntityHelper.SetCreated(newFaq);
                faq = await _repository.Add(newFaq);
            }
            else
            {
                var updateFaq = _mapper.Map<Faq>(dto);
                BaseEntityHelper.SetUpdated(updateFaq);
                faq = await _repository.Update(updateFaq);
            }

            return _mapper.Map<FaqDto>(faq);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, FaqDto dto)
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
