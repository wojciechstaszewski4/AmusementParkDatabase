using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Models;

[Index("ReservationCode", Name = "UQ__Reservat__4FCEA9B73F872BF8", IsUnique = true)]
public partial class Reservation
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int IdUsers { get; set; }

    [Column("ID_Attractions")]
    public int? IdAttractions { get; set; }

    [Column("ID_Events")]
    public int? IdEvents { get; set; }

    [Column("Reservation_Date")]
    public DateOnly ReservationDate { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [Column("Total_Cost", TypeName = "decimal(10, 2)")]
    public decimal TotalCost { get; set; }

    [Column("Number_Of_People")]
    public int NumberOfPeople { get; set; }

    [Column("Special_Request")]
    [StringLength(255)]
    public string? SpecialRequest { get; set; }

    [Column("Reservation_Code")]
    [StringLength(50)]
    public string? ReservationCode { get; set; }

    [ForeignKey("IdAttractions")]
    [InverseProperty("Reservations")]
    public virtual Attraction? IdAttractionsNavigation { get; set; }

    [ForeignKey("IdEvents")]
    [InverseProperty("Reservations")]
    public virtual Event? IdEventsNavigation { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Reservations")]
    public virtual User IdUsersNavigation { get; set; } = null!;
}
