using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Event
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Column("Start_Date", TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column("End_Date", TypeName = "datetime")]
    public DateTime EndDate { get; set; }

    [Column("Ticket_Price", TypeName = "decimal(10, 2)")]
    public decimal? TicketPrice { get; set; }

    [InverseProperty("IdEventsNavigation")]
    public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();

    [InverseProperty("IdEventsNavigation")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("IdEventsNavigation")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("IdEventsNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("IdEventsNavigation")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
