﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskListPoolLib
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TaskListPoolEntities : DbContext
    {
        public TaskListPoolEntities()
            : base("name=TaskListPoolEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskGroup> TaskGroups { get; set; }
        public virtual DbSet<TaskGroupLink> TaskGroupLinks { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<TaskGroup_GetAllWithSummary_Result> TaskGroup_GetAllWithSummary()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TaskGroup_GetAllWithSummary_Result>("TaskGroup_GetAllWithSummary");
        }

        public System.Data.Entity.DbSet<TaskListPoolLib.TaskGroup_GetAllWithSummary_Result> TaskGroup_GetAllWithSummary_Result { get; set; }
    }
}
