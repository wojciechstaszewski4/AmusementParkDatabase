using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Models;

[Index("Login", Name = "UQ__Users__5E55825BBE9ECD0F", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D105349DB1BE85", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Surname { get; set; } = null!;

    [StringLength(255)]
    public string? Address { get; set; }

    [Column("Postal_Code")]
    [StringLength(10)]
    public string? PostalCode { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [Column("Phone_Number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [Column("Birth_Date")]
    public DateOnly? BirthDate { get; set; }

    [StringLength(6)]
    public string? Gender { get; set; }

    [StringLength(50)]
    public string? Login { get; set; }

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Column("Add_Date", TypeName = "datetime")]
    public DateTime AddDate { get; set; }

    [Column("Last_Login_Date", TypeName = "datetime")]
    public DateTime? LastLoginDate { get; set; }

    [StringLength(8)]
    public string? Status { get; set; }

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Acl> Acls { get; set; } = new List<Acl>();

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    [InverseProperty("IdUsersNavigation")]
    public virtual Employee? Employee { get; set; }

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("IdUsersNavigation")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
