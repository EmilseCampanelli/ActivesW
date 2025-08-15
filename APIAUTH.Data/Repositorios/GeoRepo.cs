using APIAUTH.Data.Context;
using APIAUTH.Domain.Entities;
using APIAUTH.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Data.Repositorios
{
    public class GeoRepo : IGeoRepo
    {
        private readonly ActivesWContext _db;

        public GeoRepo(ActivesWContext db)
        {
            _db = db;
        }

        public async Task<List<Country>> GetPaisesAsync()
        {
            return await _db.Countries
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        // countryIso2: "AR", "US", etc.
        public async Task<List<Province>> GetProvinciasAsync(string countryIso2)
        {
            var iso = countryIso2?.Trim().ToUpper() ?? "";
            var countryId = await _db.Countries
                .Where(c => c.Iso2 == iso)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (countryId == 0) return new List<Province>();

            return await _db.Provinces
                .AsNoTracking()
                .Where(s => s.CountryId == countryId)
                .OrderBy(s => s.Name) 
                .ToListAsync();
        }

        // Filtros en cascada: país obligatorio, provincia opcional, q opcional, top para limitar resultados
        public async Task<List<City>> GetCiudadesAsync(string countryIso2, int? stateId = null, string? q = null, int top = 50)
        {
            var iso = countryIso2?.Trim().ToUpper() ?? "";
            var countryId = await _db.Countries
                .Where(c => c.Iso2 == iso)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
            if (countryId == 0) return new List<City>();

            var cities = _db.Cities
                .AsNoTracking()
                .Where(c => c.CountryId == countryId);

            if (stateId.HasValue)
                cities = cities.Where(c => c.ProvinceId == stateId.Value);

            if (!string.IsNullOrWhiteSpace(q))
                cities = cities.Where(c => EF.Functions.ILike(c.Name, $"%{q}%"));

            return await cities
                .OrderByDescending(c => c.Population).ThenBy(c => c.Name)
                .Take(Math.Clamp(top, 1, 500)) 
                .ToListAsync();
        }
    }
}
