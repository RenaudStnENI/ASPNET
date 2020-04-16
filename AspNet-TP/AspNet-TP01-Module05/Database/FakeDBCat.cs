using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP.Models;

namespace TP.Database
{
    public class FakeDBCat
    {
        private static FakeDBCat _instance;
        static readonly object instanceLock = new object();

        private FakeDBCat()
        {
            GetMeuteDeChats();
        }

        public static FakeDBCat Instance
        {
            get
            {
                if (_instance == null
                ) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
                {
                    lock (instanceLock)
                    {
                        if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                            _instance = new FakeDBCat();
                    }
                }

                return _instance;
            }
        }

        private List<Chat> listeChats = new List<Chat>();

        public List<Chat> ListeChats
        {
            get { return listeChats; }
        }


        private void GetMeuteDeChats()
        {
            var i = 1;
            {
                listeChats.Add(new Chat {Id = i++, Nom = "Felix", Age = 3, Couleur = "Roux"});
                listeChats.Add(new Chat {Id = i++, Nom = "Minette", Age = 1, Couleur = "Noire"});
                listeChats.Add(new Chat {Id = i++, Nom = "Miss", Age = 10, Couleur = "Blanche"});
                listeChats.Add(new Chat {Id = i++, Nom = "Garfield", Age = 6, Couleur = "Gris"});
                listeChats.Add(new Chat {Id = i++, Nom = "Chatran", Age = 4, Couleur = "Fauve"});
                listeChats.Add(new Chat {Id = i++, Nom = "Minou", Age = 2, Couleur = "Blanc"});
                listeChats.Add(new Chat {Id = i, Nom = "Bichette", Age = 12, Couleur = "Rousse"});
            }
            ;
        }
    }
}