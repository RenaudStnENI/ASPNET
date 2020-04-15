using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet_TP
{
    public class Cercle : Forme
    {
        public int Rayon { get; set; }

        public double Aire()
        {
            return Math.PI * this.Rayon * this.Rayon;
        }

        public double Perimetre()
        {
            return 2 * Math.PI * this.Rayon;
        }

        public override string ToString()
        {
            return $"Cercle de rayon {Rayon}" + Environment.NewLine +
                            $"Aire = {Aire()}" + Environment.NewLine +
                            $"Périmètre = {Perimetre()}" + Environment.NewLine;
        }
    }
}
