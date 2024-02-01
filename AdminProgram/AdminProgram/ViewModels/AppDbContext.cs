using AdminProgram.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminProgram.ViewModels
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }

        public virtual DbSet<ReservedCar> ReservedCars { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=car_db;Username=postgres;Password=root");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("cars_pkey");

                entity.ToTable("cars");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");
                entity.Property(e => e.Latitude).HasColumnName("latitude");
                entity.Property(e => e.Longitude).HasColumnName("longitude");
                entity.Property(e => e.Model)
                    .HasMaxLength(100)
                    .HasColumnName("model");
                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
                entity.Property(e => e.StateNumber)
                    .HasMaxLength(9)
                    .HasColumnName("state_number");
                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Status).WithMany(p => p.Cars)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cars_status_id_fkey");
            });

            modelBuilder.Entity<ReservedCar>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("reserved_cars_pkey");

                entity.ToTable("reserved_cars");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BookingStatus).HasColumnName("booking_status");
                entity.Property(e => e.CarId).HasColumnName("car_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.BookingStatusNavigation).WithMany(p => p.ReservedCars)
                    .HasForeignKey(d => d.BookingStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reserved_cars_booking_status_fkey");

                entity.HasOne(d => d.Car).WithMany(p => p.ReservedCars)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reserved_cars_car_id_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.ReservedCars)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("reserved_cars_user_id_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("roles_pkey");

                entity.ToTable("roles");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("statuses_pkey");

                entity.ToTable("statuses");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("subscriptions_pkey");

                entity.ToTable("subscriptions");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DriverPass)
                    .HasMaxLength(12)
                    .HasColumnName("driver_pass");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");
                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");
                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .HasColumnName("password");
                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.SubId).HasColumnName("sub_id");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("users_role_id_fkey");

                entity.HasOne(d => d.Sub).WithMany(p => p.Users)
                    .HasForeignKey(d => d.SubId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("users_sub_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}