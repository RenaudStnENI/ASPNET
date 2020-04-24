using AspNet_TP_Module05_BO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AspNet_TP02_Module05.Utils;

namespace AspNet_TP02_Module05.Models
{
    public class PizzaCreateEditVM
    {
        [PizzaNameValidator] 
        [Required]
        public Pizza Pizza { get; set; }
        public List<SelectListItem> Pate { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "Sélectionnez une pâte!")]
        [Range(1, 4, ErrorMessage = "Sélectionnez une pâte!")]
        public int IdPate { get; set; }

        [Required(ErrorMessage = "Sélectionnez des ingrédients")]
        [CountItemsValidator(2, 5)]
        [IngredientCheckValidator]
        public List<int> IdIngredients { get; set; } = new List<int>();
    }
}