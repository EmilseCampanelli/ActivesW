﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country {get; set;}
        public string ZipCode { get; set; }

    }
}
