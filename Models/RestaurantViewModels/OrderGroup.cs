using System;
using System.ComponentModel.DataAnnotations;
namespace Proiect__Savina_Ioana.Models.RestaurantViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int FoodCount { get; set; }
        public string Food { get; set; }


    }
}
