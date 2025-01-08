using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Promotion
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Attractions")]
    public int? IdAttractions { get; set; }

    [Column("ID_Events")]
    public int? IdEvents { get; set; }

    [Column("ID_Products")]
    public int? IdProducts { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Column("Start_Date", TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column("End_Date", TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    [Column("Discount_Percentage", TypeName = "decimal(5, 2)")]
    public decimal? DiscountPercentage { get; set; }

    [Column("Discount_Amount", TypeName = "decimal(10, 2)")]
    public decimal? DiscountAmount { get; set; }

    [Column("Applicable_Attractions")]
    [StringLength(255)]
    public string? ApplicableAttractions { get; set; }

    [Column("Applicable_Events")]
    [StringLength(255)]
    public string? ApplicableEvents { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [ForeignKey("IdAttractions")]
    [InverseProperty("Promotions")]
    public virtual Attraction? IdAttractionsNavigation { get; set; }

    [ForeignKey("IdEvents")]
    [InverseProperty("Promotions")]
    public virtual Event? IdEventsNavigation { get; set; }

    [ForeignKey("IdProducts")]
    [InverseProperty("Promotions")]
    public virtual Product? IdProductsNavigation { get; set; }
}
