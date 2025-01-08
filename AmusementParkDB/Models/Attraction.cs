using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Attraction
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Attraction_Categories")]
    public int IdAttractionCategories { get; set; }

    [Column("ID_Users")]
    public int? IdUsers { get; set; }

    [Column("ID_Employees")]
    public int? IdEmployees { get; set; }

    [Column("ID_Events")]
    public int? IdEvents { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    public int? Capacity { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [Column("Opening_Date")]
    public DateOnly? OpeningDate { get; set; }

    [Column("Closing_Date")]
    public DateOnly? ClosingDate { get; set; }

    [Column("Maintenance_Date")]
    public DateOnly? MaintenanceDate { get; set; }

    [Column("Ticket_Price", TypeName = "decimal(10, 2)")]
    public decimal? TicketPrice { get; set; }

    [Column("Available_Slots")]
    public int? AvailableSlots { get; set; }

    [Column("Occupied_Slots")]
    public int? OccupiedSlots { get; set; }

    [Column("Age_Restriction")]
    public int? AgeRestriction { get; set; }

    [StringLength(50)]
    public string? Supervisor { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [ForeignKey("IdAttractionCategories")]
    [InverseProperty("Attractions")]
    public virtual AttractionCategory IdAttractionCategoriesNavigation { get; set; } = null!;

    [ForeignKey("IdEmployees")]
    [InverseProperty("Attractions")]
    public virtual Employee? IdEmployeesNavigation { get; set; }

    [ForeignKey("IdEvents")]
    [InverseProperty("Attractions")]
    public virtual Event? IdEventsNavigation { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Attractions")]
    public virtual User? IdUsersNavigation { get; set; }

    [InverseProperty("IdAttractionsNavigation")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [InverseProperty("IdAttractionsNavigation")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("IdAttractionsNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("IdAttractionsNavigation")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("IdAttractionsNavigation")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
