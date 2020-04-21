using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace AspNet_TP_Module05_BO
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Donnez un nom à la pizza")]
        [StringLength(20, MinimumLength = 2)]
        public string Nom { get; set; }

        public Pate Pate { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}