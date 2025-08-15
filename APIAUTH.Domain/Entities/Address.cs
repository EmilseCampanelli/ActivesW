namespace APIAUTH.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public long CityId { get; set; }
        public virtual City City { get; set; }
        public virtual Province Province { get; set; }
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string ZipCode { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Notes { get; set; }

        public int UserId { get; set; }
        public virtual User User {get; set;}

    }
}
