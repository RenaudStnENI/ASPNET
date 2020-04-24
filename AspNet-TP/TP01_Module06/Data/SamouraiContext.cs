 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
 using BO_Samourai;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Samourai>().Ignore(s => s.Potentiel);

            modelBuilder.Entity<Samourai>().HasMany(s => s.ArtMartials).WithMany();

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<BO_Samourai.Arme> Armes { get; set; }
        public System.Data.Entity.DbSet<BO_Samourai.Samourai> Samourais { get; set; }
        public System.Data.Entity.DbSet<BO_Samourai.ArtMartial> ArtMartials { get; set; }


    }
}
