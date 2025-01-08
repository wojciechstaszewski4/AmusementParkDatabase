using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Store
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Employees")]
    public int? IdEmployees { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Location { get; set; }

    [Column("Opening_Hours")]
    [StringLength(255)]
    public string? OpeningHours { get; set; }

    [Column("Contact_Information")]
    [StringLength(255)]
    public string? ContactInformation { get; set; }

    [StringLength(50)]
    public string? Supervisor { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Rating { get; set; }

    [InverseProperty("IdStoresNavigation")]
    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    [ForeignKey("IdEmployees")]
    [InverseProperty("Stores")]
    public virtual Employee? IdEmployeesNavigation { get; set; }

    [InverseProperty("IdStoresNavigation")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("IdStoresNavigation")]
    public virtual ICollection<StoreInventory> StoreInventories { get; set; } = new List<StoreInventory>();
}
