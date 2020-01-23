using System;
using System.Collections.Generic;

namespace FungiFinder.Models.Entities
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
    }
}
