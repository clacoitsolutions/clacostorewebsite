﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OjasMart.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ozasmartEntities : DbContext
    {
        public ozasmartEntities()
            : base("name=ozasmartEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<mst_Role> mst_Role { get; set; }
        public virtual DbSet<State_Master> State_Master { get; set; }
        public virtual DbSet<tbl_Login> tbl_Login { get; set; }
        public virtual DbSet<tbl_MainMenu> tbl_MainMenu { get; set; }
        public virtual DbSet<tbl_MenuConfiguration> tbl_MenuConfiguration { get; set; }
        public virtual DbSet<tbl_SubMenu> tbl_SubMenu { get; set; }
        public virtual DbSet<tbl_VendorMaster> tbl_VendorMaster { get; set; }
        public virtual DbSet<tbl_VendorRegistration> tbl_VendorRegistration { get; set; }
    
        
    }
}
