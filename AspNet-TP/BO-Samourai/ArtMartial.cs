using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace BO_Samourai
{
    public class ArtMartial : AbstractEntity
    {
        [Required]
        public string Nom { get; set; }
    }
}
