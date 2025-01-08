using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

[Table("Order_Items")]
public partial class OrderItem
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Orders")]
    public int IdOrders { get; set; }

    [Column("ID_Attractions")]
    public int? IdAttractions { get; set; }

    [Column("ID_Events")]
    public int? IdEvents { get; set; }

    [Column("ID_Products")]
    public int? IdProducts { get; set; }

    public int Quantity { get; set; }

    [Column("Unit_Price", TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    [ForeignKey("IdAttractions")]
    [InverseProperty("OrderItems")]
    public virtual Attraction? IdAttractionsNavigation { get; set; }

    [ForeignKey("IdEvents")]
    [InverseProperty("OrderItems")]
    public virtual Event? IdEventsNavigation { get; set; }

    [ForeignKey("IdOrders")]
    [InverseProperty("OrderItems")]
    public virtual Order IdOrdersNavigation { get; set; } = null!;

    [ForeignKey("IdProducts")]
    [InverseProperty("OrderItems")]
    public virtual Product? IdProductsNavigation { get; set; }
}
