using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Build.Framework;

namespace BO_Samourai
{
    public class Samourai : AbstractEntity
    {
        [Required] 
        public int Force { get; set; }

        [Required] 
        public string Nom { get; set; }

        public virtual Arme Arme { get; set; }

        [DisplayName("Arts Martiaux")]
        public virtual List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        public int Potentiel
        {
            get
            {
                return this.Arme == null
                    ? Force * (ArtMartials.Count + 1)
                    : (Force + Arme.Degats) * (ArtMartials.Count + 1);
            }
        }

    }
}