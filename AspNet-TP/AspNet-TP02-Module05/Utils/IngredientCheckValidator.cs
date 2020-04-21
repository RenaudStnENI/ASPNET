using System;
using System.Collections;
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
            bool isDifferent = true;

            foreach (var pizza in FakeDBPizza.Instance.ListePizzas)
            {
                if (vm.IdIngredients.Count == pizza.Ingredients.Count)
                {
                    isDifferent = false;
                    List<Ingredient> pizzaDb = pizza.Ingredients.OrderBy(x => x.Id).ToList();
                    vm.IdIngredients = vm.IdIngredients.OrderBy(x => x).ToList();
                    for (int i = 0; i < vm.IdIngredients.Count; i++)
                    {
                        if (vm.IdIngredients.ElementAt(i) != pizzaDb.ElementAt(i).Id)
                        {
                            isDifferent = true;
                            break;
                        }
                    }
                }
            }

            return !isDifferent ? new ValidationResult("oops") : ValidationResult.Success;
        }
    }
}