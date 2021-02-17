using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect__Savina_Ioana.Models
{
    public class Food
    {
       
            public int ID { get; set; }
            public string Dish { get; set; }
            public string Chef { get; set; }
        public decimal Price { get; set; }
            public ICollection<Order> Orders { get; set; }
        public ICollection<OwnedFood> OwnedFoods { get; set; }


    }
}
