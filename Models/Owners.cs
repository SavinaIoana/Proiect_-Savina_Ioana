using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proiect__Savina_Ioana.Models
{
    public class Owners
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Owner Name")]
        [StringLength(50)]
        public string OwnerName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<OwnedFood> OwnedFoods { get; set; }

    }
}
