using AppDev.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppDev.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<CartItem> CartItems { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        public DbSet<NewCategoryRequest> NewCategoryRequests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>(o =>
            {
                o.HasIndex(c => c.Name).IsUnique();
            });

            builder.Entity<NewCategoryRequest>(o =>
            {
                o.HasIndex(c => c.Name).IsUnique();
            });

            builder.Entity<CartItem>(o =>
            {
                o.HasKey(ci => new { ci.BookId, ci.UserId });
            });

            builder.Entity<OrderItem>(o =>
            {
                o.HasKey(oi => new { oi.BookId, oi.OrderId });
            });

            // remove on cascade of table User to avoid multiple cascade delete paths.
            builder.Entity<Book>(o =>
            {
                o.HasOne(b => b.StoreOwner)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Order>(o =>
            {
                o.HasOne(b => b.StoreOwner)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction);

                o.HasOne(b => b.Customer)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}