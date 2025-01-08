using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

[Table("Store_Inventory")]
public partial class StoreInventory
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Stores")]
    public int IdStores { get; set; }

    [Column("ID_Products")]
    public int IdProducts { get; set; }

    [Column("Quantity_In_Stock")]
    public int? QuantityInStock { get; set; }

    [ForeignKey("IdProducts")]
    [InverseProperty("StoreInventories")]
    public virtual Product IdProductsNavigation { get; set; } = null!;

    [ForeignKey("IdStores")]
    [InverseProperty("StoreInventories")]
    public virtual Store IdStoresNavigation { get; set; } = null!;
}
