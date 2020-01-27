using System;
using System.Collections.Generic;

namespace FungiFinder.Models.Entities
{
    public partial class MapLocation
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
