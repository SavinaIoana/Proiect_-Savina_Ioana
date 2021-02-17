using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect__Savina_Ioana.Models
{
    public class OwnedFood
    {
        public int OwnerID { get; set; }
        public int FoodID { get; set; }
        public Owners Owners { get; set; }
        public Food Food { get; set; }
    }
}
