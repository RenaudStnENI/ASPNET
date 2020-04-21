using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspNet_TP02_Module05.Database;
using AspNet_TP02_Module05.Models;

namespace AspNet_TP02_Module05.Utils
{
    public class PizzaNameValidator : ValidationAttribute
    {
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PizzaCreateEditVM vm = (PizzaCreateEditVM)validationContext.ObjectInstance;

            if (FakeDBPizza.Instance.ListePizzas.Any(x=>x.Nom.ToUpper() == vm.Pizza.Nom.ToUpper()))
                return new ValidationResult($"{vm.Pizza.Nom} : ce nom de pizza existe déjà");
            return ValidationResult.Success;
        }

    }
}