using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Models;

[Index("Code", Name = "UQ__Coupons__A25C5AA7EB0DC4CD", IsUnique = true)]
public partial class Coupon
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int? IdUsers { get; set; }

    [Column("ID_Stores")]
    public int? IdStores { get; set; }

    [StringLength(20)]
    public string? Code { get; set; }

    [Column("Expiry_Date")]
    public DateOnly? ExpiryDate { get; set; }

    [Column("Discount_Percentage", TypeName = "decimal(5, 2)")]
    public decimal? DiscountPercentage { get; set; }

    [Column("Discount_Amount", TypeName = "decimal(10, 2)")]
    public decimal? DiscountAmount { get; set; }

    [Column("Single_Use")]
    [StringLength(3)]
    public string? SingleUse { get; set; }

    [Column("Multiple_Uses")]
    public int? MultipleUses { get; set; }

    [Column("Applicable_Attractions")]
    [StringLength(255)]
    public string? ApplicableAttractions { get; set; }

    [Column("Applicable_Events")]
    [StringLength(255)]
    public string? ApplicableEvents { get; set; }

    [ForeignKey("IdStores")]
    [InverseProperty("Coupons")]
    public virtual Store? IdStoresNavigation { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Coupons")]
    public virtual User? IdUsersNavigation { get; set; }
}
