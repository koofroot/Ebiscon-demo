using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EbisconDemo.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(x => x.Id);

                user.HasIndex(x => x.Email)
                .IsUnique();

                user.Property(x => x.Password)
                .IsRequired();

                user.Property(x => x.FirstName)
                .IsRequired();

                user.Property(x => x.LastName)
                .IsRequired();

                user.Property(x => x.UserType)
                .HasColumnType("VARCHAR(40)")
                .HasDefaultValue(UserType.Customer)
                .IsRequired();

                user.HasMany(x => x.Notifications)
                .WithOne(x => x.NotifyUser)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(product => 
            { 
                product.HasKey(x => x.Id);

                product.Property(x => x.SourceName)
                .HasColumnType("VARCHAR(40)")
                .IsRequired();

                product.Property(x => x.Price)
                .HasColumnType("NUMERIC(19,2)");

                product.HasOne(x => x.Rating)
                .WithOne(x => x.Product)
                .HasForeignKey<Product>(x => x.RatingId)
                .OnDelete(DeleteBehavior.Cascade);

                product.HasMany(x => x.Orders)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);            
            });

            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(x => x.Id);

                order.Property(x => x.Count)
                .IsRequired();

                order.Property(x => x.Status)
                .HasColumnType("VARCHAR(40)")
                .HasDefaultValue(OrderStatus.Created)
                .IsRequired();

                order.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

                order.HasOne(x => x.Product)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Rating>(rating => {
                rating.HasKey(x => x.Id);

                rating.Property(x => x.Count)
                .IsRequired();

                rating.Property(x => x.Rate)
                .IsRequired();

                rating.HasOne(x => x.Product)
                .WithOne(x => x.Rating)
                .HasForeignKey<Rating>(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Notification>(notification => {
                notification.HasKey(x => x.Id);

                notification.Property(x => x.Message)
                .IsRequired();

                notification.Property(x => x.IsRead)
                .HasDefaultValue(false);

                notification.HasOne(x => x.Order)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.OrderId);

                notification.HasOne(x => x.NotifyUser)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.NotifyUserId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
