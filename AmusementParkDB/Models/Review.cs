using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Review
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int? IdUsers { get; set; }

    [Column("ID_Employees")]
    public int? IdEmployees { get; set; }

    [Column("ID_Attractions")]
    public int? IdAttractions { get; set; }

    [Column("ID_Products")]
    public int? IdProducts { get; set; }

    [Column("ID_Stores")]
    public int? IdStores { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Rating { get; set; }

    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("User_Rating", TypeName = "decimal(2, 1)")]
    public decimal? UserRating { get; set; }

    [Column("Staff_Rating", TypeName = "decimal(2, 1)")]
    public decimal? StaffRating { get; set; }

    [Column("Cleanliness_Rating", TypeName = "decimal(2, 1)")]
    public decimal? CleanlinessRating { get; set; }

    [ForeignKey("IdAttractions")]
    [InverseProperty("Reviews")]
    public virtual Attraction? IdAttractionsNavigation { get; set; }

    [ForeignKey("IdEmployees")]
    [InverseProperty("Reviews")]
    public virtual Employee? IdEmployeesNavigation { get; set; }

    [ForeignKey("IdProducts")]
    [InverseProperty("Reviews")]
    public virtual Product? IdProductsNavigation { get; set; }

    [ForeignKey("IdStores")]
    [InverseProperty("Reviews")]
    public virtual Store? IdStoresNavigation { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Reviews")]
    public virtual User? IdUsersNavigation { get; set; }
}
