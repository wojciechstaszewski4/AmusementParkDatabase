using AmusementParkDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Data;

public partial class AmusementParkDbContext : DbContext
{
    public AmusementParkDbContext()
    {
    }

    public AmusementParkDbContext(DbContextOptions<AmusementParkDbContext> options)
        : base(options)
    {
    }

    public virtual required DbSet<Acl> Acls { get; set; }

    public virtual required DbSet<Agreement> Agreements { get; set; }

    public virtual required DbSet<Attraction> Attractions { get; set; }

    public virtual required DbSet<AttractionCategory> AttractionCategories { get; set; }

    public virtual required DbSet<Coupon> Coupons { get; set; }

    public virtual required DbSet<Department> Departments { get; set; }

    public virtual required DbSet<Employee> Employees { get; set; }

    public virtual required DbSet<Event> Events { get; set; }

    public virtual required DbSet<Order> Orders { get; set; }

    public virtual required DbSet<OrderItem> OrderItems { get; set; }

    public virtual required DbSet<Product> Products { get; set; }

    public virtual required DbSet<Promotion> Promotions { get; set; }

    public virtual required DbSet<Reservation> Reservations { get; set; }

    public virtual required DbSet<Review> Reviews { get; set; }

    public virtual required DbSet<Store> Stores { get; set; }

    public virtual required DbSet<StoreInventory> StoreInventories { get; set; }

    public virtual required DbSet<Ticket> Tickets { get; set; }

    public virtual required DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acl>(entity =>
        {
            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Acls).HasConstraintName("FK_ACL_Users");
        });

        modelBuilder.Entity<Agreement>(entity =>
        {
            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Agreements).HasConstraintName("FK_Agreements_Users");
        });

        modelBuilder.Entity<Attraction>(entity =>
        {
            entity.HasOne(d => d.IdAttractionCategoriesNavigation).WithMany(p => p.Attractions).HasConstraintName("FK_Attractions_Attraction_Categories");

            entity.HasOne(d => d.IdEmployeesNavigation).WithMany(p => p.Attractions).HasConstraintName("FK_Attractions_Employees");

            entity.HasOne(d => d.IdEventsNavigation).WithMany(p => p.Attractions)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Attractions_Events");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Attractions)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Attractions_Users");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.Property(e => e.MultipleUses).HasDefaultValue(1);

            entity.HasOne(d => d.IdStoresNavigation).WithMany(p => p.Coupons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Coupons_Stores");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Coupons)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Coupons_Users");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdDepartmentsNavigation).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.IdUsersNavigation).WithOne(p => p.Employee).HasConstraintName("FK_Employees_Users");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasOne(d => d.IdAttractionsNavigation).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Order_Items_Attractions");

            entity.HasOne(d => d.IdEventsNavigation).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Order_Items_Events");

            entity.HasOne(d => d.IdOrdersNavigation).WithMany(p => p.OrderItems).HasConstraintName("FK_Order_Items_Orders");

            entity.HasOne(d => d.IdProductsNavigation).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Order_Items_Products");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasOne(d => d.IdAttractionsNavigation).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Promotions_Attractions");

            entity.HasOne(d => d.IdEventsNavigation).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Promotions_Events");

            entity.HasOne(d => d.IdProductsNavigation).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Promotions_Products");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasOne(d => d.IdAttractionsNavigation).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Reservations_Attractions");

            entity.HasOne(d => d.IdEventsNavigation).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Reservations_Events");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Reservations).HasConstraintName("FK_Reservations_Users");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.IdAttractionsNavigation).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Reviews_Attractions");

            entity.HasOne(d => d.IdEmployeesNavigation).WithMany(p => p.Reviews).HasConstraintName("FK_Reviews_Employees");

            entity.HasOne(d => d.IdProductsNavigation).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Reviews_Products");

            entity.HasOne(d => d.IdStoresNavigation).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Reviews_Stores");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasOne(d => d.IdEmployeesNavigation).WithMany(p => p.Stores)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Stores_Employees");
        });

        modelBuilder.Entity<StoreInventory>(entity =>
        {
            entity.Property(e => e.QuantityInStock).HasDefaultValue(0);

            entity.HasOne(d => d.IdProductsNavigation).WithMany(p => p.StoreInventories).HasConstraintName("FK_Store_Inventory_Products");

            entity.HasOne(d => d.IdStoresNavigation).WithMany(p => p.StoreInventories).HasConstraintName("FK_Store_Inventory_Stores");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasOne(d => d.IdAttractionsNavigation).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Tickets_Attractions");

            entity.HasOne(d => d.IdEventsNavigation).WithMany(p => p.Tickets)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Tickets_Events");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.AddDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
