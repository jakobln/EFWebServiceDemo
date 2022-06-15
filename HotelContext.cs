namespace EFWebServiceDemo
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelContext : DbContext
    {
        public HotelContext()
            : base("name=HotelContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<DemoBooking> DemoBookings { get; set; }
        
        public virtual DbSet<DemoGuest> DemoGuests { get; set; }
        public virtual DbSet<DemoHotel> DemoHotels { get; set; }
        public virtual DbSet<DemoRoom> DemoRooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
       
            modelBuilder.Entity<DemoGuest>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DemoGuest>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<DemoGuest>()
                .HasMany(e => e.DemoBookings)
                .WithRequired(e => e.DemoGuest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemoHotel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DemoHotel>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<DemoHotel>()
                .HasMany(e => e.DemoRooms)
                .WithRequired(e => e.DemoHotel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DemoRoom>()
                .Property(e => e.Types)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DemoRoom>()
                .HasMany(e => e.DemoBookings)
                .WithRequired(e => e.DemoRoom)
                .HasForeignKey(e => new { e.Room_No, e.Hotel_No })
                .WillCascadeOnDelete(false);
        }
    }
}
