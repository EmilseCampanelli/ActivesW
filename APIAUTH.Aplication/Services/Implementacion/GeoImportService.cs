using APIAUTH.Aplication.Services.Interfaces;
using APIAUTH.Data.Context;
using APIAUTH.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Services.Implementacion
{
    public class GeoImportService : IGeoImportService
    {
        private readonly ActivesWContext _db;
        public GeoImportService(ActivesWContext db) => _db = db;

        public async Task ImportAsync(string folderPath, CancellationToken ct = default)
        {

            await _db.Database.ExecuteSqlRawAsync(@"TRUNCATE ""Cities"" RESTART IDENTITY CASCADE;");
            await _db.Database.ExecuteSqlRawAsync(@"TRUNCATE ""Province"" RESTART IDENTITY CASCADE;");
            await _db.Database.ExecuteSqlRawAsync(@"TRUNCATE ""Countries"" RESTART IDENTITY CASCADE;");

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"GeoImport folder not found: {folderPath}");

            var countryInfo = Path.Combine(folderPath, "countryInfo.txt");
            var admin1Path = Path.Combine(folderPath, "admin1CodesASCII.txt");
            var citiesPath = Path.Combine(folderPath, "cities5000.txt"); // o cities15000.txt

            if (!File.Exists(countryInfo)) throw new FileNotFoundException(countryInfo);
            if (!File.Exists(admin1Path)) throw new FileNotFoundException(admin1Path);
            if (!File.Exists(citiesPath)) throw new FileNotFoundException(citiesPath);

            // 1) COUNTRIES (solo NA/SA) ----------------------------------------
            if (!await _db.Countries.AnyAsync(ct))
            {
                var countries = LoadCountries(countryInfo)
                    .Where(c => c.Continent == "NA" || c.Continent == "SA")
                    .Select(c => new Country
                    {
                        Iso2 = c.Iso2,
                        Iso3 = c.Iso3,
                        Name = c.Name,
                        Region = "Americas",
                        GeoNameId = c.GeoNameId
                    })
                    .ToList();

                _db.Countries.AddRange(countries);
                await _db.SaveChangesAsync(ct);
            }

            // Map ISO2 -> CountryId
            var countryMap = await _db.Countries
                .ToDictionaryAsync(c => c.Iso2, c => c.Id, ct);

            // 2) STATES/PROVINCES ----------------------------------------------
            if (!await _db.Provinces.AnyAsync(ct))
            {
                var statesRaw = LoadAdmin1(admin1Path);
                var states = new List<Province>(capacity: 4000);

                foreach (var s in statesRaw)
                {
                    var parts = s.Code.Split('.'); // "AR.24" => ["AR","24"]
                    if (parts.Length != 2) continue;
                    var iso2 = parts[0];
                    if (!countryMap.TryGetValue(iso2, out var countryId)) continue;

                    states.Add(new Province
                    {
                        CountryId = countryId,
                        Code = s.Code,     // "AR.24"
                        Name = s.Name,     // Tucumán
                        GeoNameId = s.GeoNameId
                    });
                }

                foreach (var chunk in Chunk(states, 1000))
                {
                    _db.Provinces.AddRange(chunk);
                    await _db.SaveChangesAsync(ct);
                }
            }

            // Map admin1 code -> StateId (ej "AR.24")
            var stateMap = await _db.Provinces
                .ToDictionaryAsync(s => s.Code, s => s.Id, ct);

            // 3) CITIES ---------------------------------------------------------
            if (!await _db.Cities.AnyAsync(ct))
            {
                var ci = LoadCities(citiesPath);
                var cities = new List<City>(capacity: 150_000);

                foreach (var c in ci)
                {
                    if (!countryMap.TryGetValue(c.Iso2, out var countryId)) continue;

                    stateMap.TryGetValue($"{c.Iso2}.{c.Admin1}", out var stateId);

                    cities.Add(new City
                    {
                        Id = c.GeoNameId, // PK = geonameid
                        CountryId = countryId,
                        ProvinceId = stateId == 0 ? null : stateId,
                        Name = c.Name,
                        Lat = c.Lat,
                        Lng = c.Lng,
                        Population = c.Population
                    });
                }

                foreach (var chunk in Chunk(cities, 5000))
                {
                    _db.Cities.AddRange(chunk);
                    await _db.SaveChangesAsync(ct);
                }
            }
        }

        // ---------------------- Parsers ----------------------
        private static IEnumerable<(string Iso2, string Iso3, string Name, string Continent, int GeoNameId)>
            LoadCountries(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                var p = line.Split('\t');
                // 0=ISO, 1=ISO3, 4=Country, 8=Continent, 16=geonameid
                yield return (
                    Iso2: Safe(p, 0),
                    Iso3: Safe(p, 1),
                    Name: Safe(p, 4),
                    Continent: Safe(p, 8),
                    GeoNameId: int.Parse(Safe(p, 16))
                );
            }
        }

        private static IEnumerable<(string Code, string Name, int GeoNameId)>
            LoadAdmin1(string path)
        {
            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var p = line.Split('\t');
                // code \t name \t nameAscii \t geonameid
                if (p.Length < 4) continue;
                yield return (Code: p[0], Name: p[1], GeoNameId: int.Parse(p[3]));
            }
        }

        private static IEnumerable<(long GeoNameId, string Name, string Iso2, string Admin1, decimal Lat, decimal Lng, int Population)>
            LoadCities(string path)
        {
            var ci = CultureInfo.InvariantCulture;
            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                var p = line.Split('\t');
                // 0=id,1=name,2=asciiname,4=lat,5=lon,8=country code,10=admin1 code,14=population
                if (p.Length < 15) continue;
                yield return (
                    GeoNameId: long.Parse(p[0]),
                    Name: p[1],
                    Iso2: p[8],
                    Admin1: p[10],
                    Lat: decimal.Parse(p[4], ci),
                    Lng: decimal.Parse(p[5], ci),
                    Population: int.Parse(p[14])
                );
            }
        }

        private static string Safe(string[] arr, int idx) => idx < arr.Length ? arr[idx] : string.Empty;

        private static IEnumerable<List<T>> Chunk<T>(IEnumerable<T> source, int size)
        {
            var bucket = new List<T>(size);
            foreach (var item in source)
            {
                bucket.Add(item);
                if (bucket.Count == size)
                {
                    yield return bucket;
                    bucket = new List<T>(size);
                }
            }
            if (bucket.Count > 0) yield return bucket;
        }
    }
}

