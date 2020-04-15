using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet_TP
{
    public class Carre : Forme


    {
        public int Longueur { get; set; }
        public double Aire()
        {
            return Math.Pow(this.Longueur, 2);
        }

        public double Perimetre()
        {
            return this.Longueur * 4;
        }

        public override string ToString()
        {
            return $"Cercle de côté {Longueur}" + Environment.NewLine +
                            $"Aire = {Aire()}" + Environment.NewLine +
                            $"Périmètre = {Perimetre()}" + Environment.NewLine;
        }
    }
}
