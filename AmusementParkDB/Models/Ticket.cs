using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Models;

[Index("TicketNumber", Name = "UQ__Tickets__2F695419400EBF8B", IsUnique = true)]
public partial class Ticket
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

    [Column("Ticket_Number")]
    [StringLength(50)]
    public string? TicketNumber { get; set; }

    [Column("Purchase_Date")]
    public DateOnly PurchaseDate { get; set; }

    [Column("Expiry_Date")]
    public DateOnly? ExpiryDate { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("Ticket_Type")]
    [StringLength(50)]
    public string? TicketType { get; set; }

    [ForeignKey("IdAttractions")]
    [InverseProperty("Tickets")]
    public virtual Attraction? IdAttractionsNavigation { get; set; }

    [ForeignKey("IdEvents")]
    [InverseProperty("Tickets")]
    public virtual Event? IdEventsNavigation { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Tickets")]
    public virtual User IdUsersNavigation { get; set; } = null!;
}
