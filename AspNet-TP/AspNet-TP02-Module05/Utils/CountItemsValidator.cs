using System.ComponentModel.DataAnnotations;
using AspNet_TP02_Module05.Models;

namespace AspNet_TP02_Module05.Utils
{
    public class CountItemsValidator : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public CountItemsValidator(int min, int max)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PizzaCreateEditVM vm = (PizzaCreateEditVM) validationContext.ObjectInstance;

            if (_min > vm.IdIngredients.Count || vm.IdIngredients.Count > _max)
                return new ValidationResult($"Vous devez choisir entre {_min} et {_max} ingrédients");
            return ValidationResult.Success;
        }
    }
}