using FungiFinder.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungiFinder.Models
{
    public class FunctionsService
    {
        private readonly FungiFinderContext context;

        public FunctionsService(FungiFinderContext context)
        {
            this.context = context;
        }
    }
}
