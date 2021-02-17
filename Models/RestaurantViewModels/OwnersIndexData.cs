using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect__Savina_Ioana.Models.RestaurantViewModels
{
    public class OwnersIndexData
    {
        public IEnumerable<Owners> Owners { get; set; }
        public IEnumerable<Food> Foods { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
