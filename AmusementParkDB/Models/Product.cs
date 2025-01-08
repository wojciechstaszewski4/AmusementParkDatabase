using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Product
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("Stock_Quantity")]
    public int StockQuantity { get; set; }

    [StringLength(12)]
    public string? Status { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Rating { get; set; }

    [InverseProperty("IdProductsNavigation")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("IdProductsNavigation")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("IdProductsNavigation")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("IdProductsNavigation")]
    public virtual ICollection<StoreInventory> StoreInventories { get; set; } = new List<StoreInventory>();
}
