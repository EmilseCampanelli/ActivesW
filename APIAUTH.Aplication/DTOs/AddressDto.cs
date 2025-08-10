namespace APIAUTH.Aplication.DTOs
{
    public class AddressDto : BaseEntityDto
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Notes { get; set; }
        public string State { get; set; }

    }
}
