using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TP01_Module06.Data
{
    public class SamouraiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SamouraiContext() : base("name=SamouraiContext")
        {
        }

        public System.Data.Entity.DbSet<BO_Samourai.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<BO_Samourai.Arme> Armes { get; set; }
    }
}
