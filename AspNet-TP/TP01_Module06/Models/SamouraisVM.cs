using System.Collections.Generic;
using System.Web.Mvc;
using BO_Samourai;

namespace TP01_Module06.Models
{
    public class SamouraisVM
    {
        public Samourai Samourai { get; set; }

        public List<SelectListItem> ArmeItems { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ArtMartialItems { get; set; } = new List<SelectListItem>();

        public int? IdArme { get; set; }
        public List<int> IdsArtMartial { get; set; } = new List<int>();

    }
}