﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Deiofiber
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DeiofiberEntities : DbContext
    {
        public DeiofiberEntities()
            : base("name=DeiofiberEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountPermission> AccountPermissions { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractHistory> ContractHistories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InOut> InOuts { get; set; }
        public virtual DbSet<InOutType> InOutTypes { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<PayPeriod> PayPeriods { get; set; }
        public virtual DbSet<RentType> RentTypes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreFee> StoreFees { get; set; }
        public virtual DbSet<CONTRACT_FULL_VW> CONTRACT_FULL_VW { get; set; }
        public virtual DbSet<CONTRACT_HISTORY_FULL_VW> CONTRACT_HISTORY_FULL_VW { get; set; }
        public virtual DbSet<INOUT_FULL_VW> INOUT_FULL_VW { get; set; }
        public virtual DbSet<STORE_FULL_VW> STORE_FULL_VW { get; set; }
    }
}