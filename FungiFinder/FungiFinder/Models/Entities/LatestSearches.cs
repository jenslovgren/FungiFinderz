using System;
using System.Collections.Generic;

namespace FungiFinder.Models.Entities
{
    public partial class LatestSearches
    {
        public int Id { get; set; }
        public DateTime SearchDate { get; set; }
        public string UserId { get; set; }
        public string Mushroom { get; set; }
        public string ImageUrl { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
