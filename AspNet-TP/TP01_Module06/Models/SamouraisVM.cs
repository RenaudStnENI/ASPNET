using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BO_Samourai;

namespace TP01_Module06.Models
{
    public class SamouraisVM
    {
        public Samourai Samourai { get; set; }

        public List<SelectListItem> Armes { get; set; } = new List<SelectListItem>();

        public int? IdArme { get; set; }
    }
}