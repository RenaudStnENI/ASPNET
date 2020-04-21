using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspNet_TP_Module05_BO;
using AspNet_TP02_Module05.Database;
using AspNet_TP02_Module05.Models;

namespace AspNet_TP02_Module05.Utils
{
    public class IngredientCheckValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PizzaCreateEditVM vm = (PizzaCreateEditVM) validationContext.ObjectInstance;

            List<Pizza> pizzas = FakeDBPizza.Instance.ListePizzas;
            List<List<Ingredient>> recettes = pizzas.Select(p => p.Ingredients).ToList();
            List<List<int>> liste = recettes.Select(r => r.Select(i => i.Id).ToList()).ToList();

            List<int> vmIng = new List<int>();
            vmIng = vm.IdIngredients;

            return liste.Any(l=>l== vmIng) ? new ValidationResult("oops") : ValidationResult.Success;
        }
    }
}