﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWSZ_WCF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class aplikacjaPWSZEntities : DbContext
    {
        public aplikacjaPWSZEntities()
            : base("name=aplikacjaPWSZEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Budynki> Budynki { get; set; }
        public virtual DbSet<Dojazd> Dojazd { get; set; }
        public virtual DbSet<Konsultacje> Konsultacje { get; set; }
        public virtual DbSet<Kontakt> Kontakt { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Terminarz> Terminarz { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
