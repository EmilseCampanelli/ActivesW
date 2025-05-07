using APIAUTH.Aplication.DTOs;
using APIAUTH.Aplication.Helpers;
using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using AutoMapper;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class OrganizationService : ICompanyService
    {
        private readonly IRepository<Company> _organizationRepository;
        private readonly IMapper _mapper;

        public OrganizationService(IRepository<Company> organizationRepository, IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _mapper = mapper;
        }

        public async Task Activate(int id)
        {
            var organization = await _organizationRepository.Get(id);
            BaseEntityHelper.SetActive(organization);
            await _organizationRepository.Update(organization);
        }

        public async Task<bool> Exists(int id)
        {
            return await _organizationRepository.Get(id) != null;
        }

        public async Task<CompanyDto> Get(int id)
        {
            var model = await _organizationRepository.Get(id);
            return _mapper.Map<CompanyDto>(model);
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var organizationsDto = new List<CompanyDto>();
            var organizations = await _organizationRepository.GetAll();

            foreach (var organ in organizations)
            {
                organizationsDto.Add(_mapper.Map<CompanyDto>(organ));
            }

            return organizationsDto;
        }

        public async Task Inactivate(int id)
        {
            var organization = await _organizationRepository.Get(id);
            BaseEntityHelper.SetInactive(organization);
            await _organizationRepository.Update(organization);
        }

        public async Task<CompanyDto> Save(CompanyDto dto)
        {
            var organization = new Company();

            if (dto.Id.Equals(0))
            {
                var newOrganization = _mapper.Map<Company>(dto);
                BaseEntityHelper.SetCreated(newOrganization);
                organization = await _organizationRepository.Add(newOrganization);
            }
            else
            {
                var updateOrganization = _mapper.Map<Company>(dto);
                BaseEntityHelper.SetUpdated(updateOrganization);
                organization = await _organizationRepository.Update(updateOrganization);
            }

            return _mapper.Map<CompanyDto>(organization);
        }

        public async Task<(bool isValid, string message)> Validate(int? id, CompanyDto dto)
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
