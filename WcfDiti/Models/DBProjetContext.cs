using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WcfDiti.Models
{
    public class DBProjetContext:DbContext
    {
        public DBProjetContext() : base("conn") { }

        public DbSet<Personne> personnes { get; set; }
        //public DbSet<Profil> profils { get; set; }
    }
}