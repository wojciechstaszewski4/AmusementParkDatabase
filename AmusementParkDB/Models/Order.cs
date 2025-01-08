using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Order
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int? IdUsers { get; set; }

    [Column("Order_Date", TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column("Total_Price", TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Orders")]
    public virtual User? IdUsersNavigation { get; set; }

    [InverseProperty("IdOrdersNavigation")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
