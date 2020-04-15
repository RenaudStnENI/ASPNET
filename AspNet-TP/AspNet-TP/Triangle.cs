using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet_TP
{
    public class Triangle : Forme
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public double Aire()
        {
            double p = Perimetre() / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }

        private int Perimetre()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Triangle de côté A={A}, B={B}, C={C}" + Environment.NewLine +
                $"Aire = {Aire()}" + Environment.NewLine +
                $"Périmètre = {Perimetre()}" + Environment.NewLine;
        }

        double Forme.Perimetre()
        {
            throw new NotImplementedException();
        }
    }
}
