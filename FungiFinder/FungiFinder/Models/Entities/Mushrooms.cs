using System;
using System.Collections.Generic;

namespace FungiFinder.Models.Entities
{
    public partial class Mushrooms
    {
        public Mushrooms()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }
        public string LatinName { get; set; }
        public string Info { get; set; }
        public string ImageUrl { get; set; }
        public bool Edible { get; set; }
        public int? Rating { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
