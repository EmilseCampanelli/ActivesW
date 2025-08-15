using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }                 // PK interno
        public string Iso2 { get; set; } = null!;   // AR, US, BR
        public string Iso3 { get; set; } = null!;   // ARG, USA, BRA
        public string Name { get; set; } = null!;
        public string Region { get; set; } = "Americas"; // "Americas" (NA/SA)
        public int GeoNameId { get; set; }          // id geonames (countryInfo)
        public ICollection<Province> Province { get; set; } = new List<Province>();
    }

    public class Province
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; } = null!;   // p.ej. "AR.24" (admin1Codes), o ISO-3166-2 si luego lo agregás
        public string Name { get; set; } = null!;
        public int GeoNameId { get; set; }          // id geonames (admin1Codes)
        public Country Country { get; set; } = null!;
        public ICollection<City> Cities { get; set; } = new List<City>();
    }

    public class City
    {
        public long Id { get; set; }                // geonameid (sirve como PK)
        public int CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public int Population { get; set; }
        public Country Country { get; set; } = null!;
        public Province? Province { get; set; }
    }

}
