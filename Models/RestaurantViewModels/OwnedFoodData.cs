using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect__Savina_Ioana.Models.RestaurantViewModels
{
    public class OwnedFoodData
    {
        public int FoodID { get; set; }
        public string Dish { get; set; }
        public bool IsOwned { get; set; }
    }
}
