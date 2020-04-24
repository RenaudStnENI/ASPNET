using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AspNet_TP_Module05_BO;
using AspNet_TP02_Module05.Database;
using AspNet_TP02_Module05.Models;

namespace AspNet_TP02_Module05.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            ViewBag.nbPizza = FakeDBPizza.Instance.ListePizzas.Count();
            return View(FakeDBPizza.Instance.ListePizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = FakeDBPizza.Instance.ListePizzas.FirstOrDefault(x => x.Id == id);

            if (pizza != null)
            {
                PizzaCreateEditVM vm = new PizzaCreateEditVM();
                vm.Pizza = pizza;
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            return View(this.getPatesIngredients(vm));
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateEditVM vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pizza pizza = vm.Pizza;

                    pizza.Id = FakeDBPizza.Instance.ListePizzas.Count() == 0
                        ? 1
                        : FakeDBPizza.Instance.ListePizzas.Max(p => p.Id) + 1;

                    pizza.Pate = FakeDBPizza.Instance.ListePates.FirstOrDefault(p => p.Id == vm.IdPate);

                    foreach (var idIngredient in vm.IdIngredients)
                    {
                        pizza.Ingredients.Add(
                            FakeDBPizza.Instance.ListeIngredients.FirstOrDefault(i => i.Id == idIngredient));
                    }

                    FakeDBPizza.Instance.ListePizzas.Add(pizza);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(this.getPatesIngredients(vm));
                }
            }
            catch
            {
                return View(this.getPatesIngredients(vm));
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            foreach (Pate pate in FakeDBPizza.Instance.ListePates)
            {
            }

            Pizza pizza = FakeDBPizza.Instance.ListePizzas.FirstOrDefault(x => x.Id == id);

            if (pizza != null)
            {
                vm.Pizza = pizza;
                vm.IdIngredients = vm.Pizza.Ingredients.Select(i => i.Id).ToList();
                vm.IdPate = vm.Pizza.Pate.Id;
                getPatesIngredients(vm);
                return View(vm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PizzaCreateEditVM vm)
        {
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        Pizza modifiedPizza = FakeDBPizza.Instance.ListePizzas.FirstOrDefault(p => p.Id == vm.Pizza.Id);

                        // nom
                        modifiedPizza.Nom = vm.Pizza.Nom;

                        //pate
                        Pate pate = FakeDBPizza.Instance.ListePates.FirstOrDefault(p => p.Id == vm.IdPate);
                        modifiedPizza.Pate = pate;

                        //ingrédients
                        modifiedPizza.Ingredients = FakeDBPizza.Instance.ListeIngredients
                            .Where(i => vm.IdIngredients.Contains(i.Id)).ToList();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        getPatesIngredients(vm);
                        return View(vm);
                    }
                   
                }
                catch
                {
                    getPatesIngredients(vm);
                    return View(vm);
                }
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
            vm.Pizza = FakeDBPizza.Instance.ListePizzas.FirstOrDefault(p => p.Id == id);
            return View(vm);
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                FakeDBPizza.Instance.ListePizzas.Remove(FakeDBPizza.Instance.ListePizzas.FirstOrDefault(p => p.Id == id));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private PizzaCreateEditVM getPatesIngredients(PizzaCreateEditVM vm)
        {
            vm.Pate = FakeDBPizza.Instance.ListePates.Select(p => new SelectListItem()
                    {Text = p.Nom, Value = p.Id.ToString()})
                .ToList();

            vm.Ingredients = FakeDBPizza.Instance.ListeIngredients.Select(i => new SelectListItem()
                    {Text = i.Nom, Value = i.Id.ToString()})
                .ToList();
            return vm;
        }
    }
}