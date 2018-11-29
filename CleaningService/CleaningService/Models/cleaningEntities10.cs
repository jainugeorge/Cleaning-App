namespace CleaningService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class cleaningEntities10 : DbContext
    {
        public cleaningEntities10()
            : base("name=cleaningEntities10")
        {
        }

        public virtual DbSet<BookingService> BookingServices { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ServiceAvailable> ServiceAvailables { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VirtualBank> VirtualBanks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
