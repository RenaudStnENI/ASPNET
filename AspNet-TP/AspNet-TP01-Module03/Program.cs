using AspNet_TP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet_TP
{
    class Program
    {

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

        static void Main(string[] args)
        {
            InitialiserDatas();

            var auteursNameG = ListeAuteurs.Where(a => a.Nom.ToUpper().StartsWith("G"));
            Console.WriteLine("///Afficher la liste des prénoms des auteurs dont le nom commence par G");
            foreach (var auteur in auteursNameG)
            {
                Console.WriteLine($"Prénom = {auteur.Prenom}");
            }
            Console.WriteLine();

            ///
            Console.WriteLine("///l auteur ayant écrit le plus de livres");
            var auteurPlusDeLivres = ListeAuteurs.OrderByDescending(a => ListeLivres.Count(l => l.Auteur == a)).FirstOrDefault();
            Console.WriteLine($"{auteurPlusDeLivres.Prenom} {auteurPlusDeLivres.Nom}");
            Console.WriteLine();

            ///
            Console.WriteLine("///le nombre moyen de pages par livre par auteur");
            var livresParAuteur = ListeLivres.GroupBy(l => l.Auteur);
            foreach (var livre in livresParAuteur)
            {
                var nbMoyenPages = livre.Average(l => l.NbPages);
                Console.WriteLine($" auteur : {livre.Key.Nom} {livre.Key.Prenom} => nb moyen de pages : {nbMoyenPages}");
            }
            Console.WriteLine();

            ///
            Console.WriteLine("///le titre du livre avec le plus de pages");
            var livrePlusDePages = ListeLivres.OrderByDescending(l => l.NbPages).FirstOrDefault();
            Console.WriteLine($"{livrePlusDePages.Titre}");
            Console.WriteLine();

            ///
            Console.WriteLine("combien ont gagné les auteurs en moyenne (moyenne des");
            var toutesLesFactures = ListeAuteurs.SelectMany(a => a.Factures);

            Console.WriteLine($"moyenne : {toutesLesFactures.Average(f => f.Montant)}");
            Console.WriteLine();

            ///
            Console.WriteLine("les auteurs e t la liste de leurs livres");
            var auteurs = ListeLivres.Select(l => l.Auteur).Distinct();
            foreach (var auteur in auteurs)
            {
                Console.WriteLine($"auteur : {auteur.Prenom} {auteur.Nom}");
                var livres = ListeLivres.GroupBy(l => l.Titre).Where(l => ListeAuteurs.Distinct() == auteur);
                foreach (var livre in livres)
                {
                    Console.WriteLine($"{livre}");
                }
            }
            Console.WriteLine();

            ///
            Console.WriteLine("///les titres de tous les livres triés par ordre alphabétique");
            var livresAlpha = ListeLivres.Select(l => l.Titre).OrderBy(t => t);
            foreach(var livre in livresAlpha)
            {
                Console.WriteLine($"{livre}");

            }
            Console.WriteLine();

            ///
            Console.WriteLine("///la liste des livres dont le nombre de page s est supérieur à la moyenne");
            var livresPagesSuperieur = ListeLivres.Where(l => l.NbPages > ListeLivres.Average(li => li.NbPages));
            foreach (var livre in livresPagesSuperieur)
            {
                Console.WriteLine($"{livre.Titre}");
            }
            Console.WriteLine();

            ///
            Console.WriteLine("///l'auteur ayant écrit le moins de livres");
            var auteurMoinsDeLivres = ListeAuteurs.OrderBy(a => ListeLivres.Count(l => l.Auteur == a)).FirstOrDefault();
            Console.WriteLine($"{auteurMoinsDeLivres.Prenom} {auteurMoinsDeLivres.Nom}");
            Console.WriteLine();

            ///


            Console.ReadKey();
        }

    }
}

