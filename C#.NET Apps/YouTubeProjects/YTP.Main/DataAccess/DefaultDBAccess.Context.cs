﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YTP.Main.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Acc_CityData> Acc_CityData { get; set; }
        public virtual DbSet<Acc_EmpData> Acc_EmpData { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<TBS_Employee> TBS_Employee { get; set; }
        public virtual DbSet<tbl_Employee> tbl_Employee { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<RoySecHR_Employee> RoySecHR_Employee { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
