using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.DTOs
{
    public class AddressAddDto
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }
        public string PostalCode { get; set; }
        public string Apartment {  get; set; }
        public string Floor { get; set; }
        public string Notes { get; set; }
    }
}
