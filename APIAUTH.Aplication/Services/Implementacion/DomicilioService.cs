using APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser;
using APIAUTH.Aplication.DTOs;
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
    public class DomicilioService : IDomicilioService
    {
        private readonly IRepository<Domicilio> _repository;
        private readonly IMapper _mapper;

        public DomicilioService(IRepository<Domicilio> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddToUsuarioAsync(int usuarioId, CreateDomicilioCommand dto)
        {
            var existe = _repository.GetFiltered(d =>
                d.UsuarioId == usuarioId &&
                d.Calle.ToLower() == dto.Calle.ToLower() &&
                d.Numero.ToLower() == dto.Numero.ToLower() &&
                d.CodigoPostal.ToLower() == dto.CodigoPostal.ToLower()
            ).FirstOrDefault();

            if (existe != null)
                throw new ApplicationException("Ya existe un domicilio igual para este usuario.");

            var domicilio = _mapper.Map<Domicilio>(dto);
            domicilio.UsuarioId = usuarioId;

            var agregado = await _repository.Add(domicilio);
            return agregado.Id;
        }

        public async Task DeleteAsync(int domicilioId)
        {
            var entity = await _repository.Get(domicilioId);
            if (entity == null)
                throw new KeyNotFoundException("Domicilio no encontrado");

            await _repository.Delete(entity);
        }

        public async Task<IEnumerable<DomicilioDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var entity = _repository.GetFiltered(e => e.UsuarioId == usuarioId).ToList();
            var domiciliodto = _mapper.Map<List<DomicilioDto>>(entity);

            return domiciliodto;

        }

        public async Task UpdateAsync(DomicilioDto dto)
        {
            var domicilio = await _repository.Get(dto.Id);
            if (domicilio == null)
                throw new KeyNotFoundException("Domicilio no encontrado");

            _mapper.Map(dto, domicilio);
            await _repository.Update(domicilio);
        }
    }
}

