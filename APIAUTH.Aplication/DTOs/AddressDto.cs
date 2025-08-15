namespace APIAUTH.Aplication.DTOs
{
    public class AddressDto : BaseEntityDto
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Notes { get; set; }

    }
}
