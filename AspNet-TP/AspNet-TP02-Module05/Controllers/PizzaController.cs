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
            return View(this.getPatesIngredients());
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

                    if (FakeDBPizza.Instance.ListePizzas.Count() == 0)
                    {
                        pizza.Id = 1;
                    }
                    else
                    {
                        pizza.Id = FakeDBPizza.Instance.ListePizzas.Max(p => p.Id) + 1;
                    }

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
                    return View(this.getPatesIngredients());
                }
            }
            catch
            {
                return View(this.getPatesIngredients());
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
                    FakeDBPizza.Instance.ListePizzas[id].Nom = vm.Pizza.Nom;
                    FakeDBPizza.Instance.ListePizzas[id].Pate = vm.Pizza.Pate;
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private PizzaCreateEditVM getPatesIngredients()
        {
            PizzaCreateEditVM vm = new PizzaCreateEditVM();
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