using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet_TP
{
    public class Rectangle : Forme
    {

        public double Longueur { get; set; }
        public double Largeur { get; set; }

        public double Aire()
        {
            return this.Longueur * this.Largeur;
        }

        public double Perimetre()
        {
            return (this.Longueur * 2) + (this.Largeur * 2);
        }

        public override string ToString()
        {
            return $"Rectangle de longueur ={Longueur} et largeur={Largeur}" + Environment.NewLine +
                            $"Aire = {Aire()}" + Environment.NewLine +
                            $"Périmètre = {Perimetre()}" + Environment.NewLine;
        }
    }
}
