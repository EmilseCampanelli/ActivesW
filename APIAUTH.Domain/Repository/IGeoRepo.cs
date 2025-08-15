using APIAUTH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Repository
{
    public interface IGeoRepo
    {
        Task<List<Country>> GetPaisesAsync();
        Task<List<Province>> GetProvinciasAsync(string countryIso2);
        Task<List<City>> GetCiudadesAsync(string countryIso2, int? stateId = null, string? q = null, int top = 50);
    }
}
