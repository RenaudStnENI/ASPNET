using AspNet_TP_Module05_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNet_TP02_Module05.Database
{
    public class FakeDBPizza
    {
        private static FakeDBPizza _instance;
        static readonly object instanceLock = new object();

        private FakeDBPizza()
        {
            GetPizzas();
            GetIngredients();
            GetPates();
        }

        public static FakeDBPizza Instance
        {
            get
            {
                if (_instance == null
                ) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDBPizza();
                    }
                }

                return _instance;
            }
        }

        private List<Pizza> listePizzas;

        public List<Pizza> ListePizzas
        {
            get { return listePizzas; }
            set { listePizzas = value; }
        }


        private List<Pizza> GetPizzas()
        {
            return listePizzas = new List<Pizza>();
        }


        private List<Ingredient> listeIngredients;

        public List<Ingredient> ListeIngredients
        {
            get { return listeIngredients; }
        }


        private List<Ingredient> GetIngredients()
        {
            return listeIngredients = new List<Ingredient>
            {
                new Ingredient {Id = 1, Nom = "Mozzarella"},
                new Ingredient {Id = 2, Nom = "Jambon"},
                new Ingredient {Id = 3, Nom = "Tomate"},
                new Ingredient {Id = 4, Nom = "Oignon"},
                new Ingredient {Id = 5, Nom = "Cheddar"},
                new Ingredient {Id = 6, Nom = "Saumon"},
                new Ingredient {Id = 7, Nom = "Champignon"},
                new Ingredient {Id = 8, Nom = "Poulet"}
            };
        }

        private List<Pate> listePates;

        public List<Pate> ListePates
        {
            get { return listePates; }
        }


        private List<Pate> GetPates()
        {
            return listePates = new List<Pate>
            {
                new Pate {Id = 0, Nom = ""},
                new Pate {Id = 1, Nom = "Pate fine, base crême"},
                new Pate {Id = 2, Nom = "Pate fine, base tomate"},
                new Pate {Id = 3, Nom = "Pate épaisse, base crême"},
                new Pate {Id = 4, Nom = "Pate épaisse, base tomate"}
            };
        }
    }
}