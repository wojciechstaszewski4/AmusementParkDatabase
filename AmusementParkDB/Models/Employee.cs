using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Models;

[Index("IdUsers", Name = "UQ__Employee__B97FFDA04ABABF2E", IsUnique = true)]
public partial class Employee
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int IdUsers { get; set; }

    [Column("ID_Departments")]
    public int? IdDepartments { get; set; }

    [Column("Job_Title")]
    [StringLength(50)]
    public string? JobTitle { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Salary { get; set; }

    [Column("Employment_Date")]
    public DateOnly EmploymentDate { get; set; }

    [Column("Termination_Date")]
    public DateOnly? TerminationDate { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [Column("Emergency_Contact")]
    [StringLength(255)]
    public string? EmergencyContact { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Rating { get; set; }

    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [InverseProperty("IdEmployeesNavigation")]
    public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();

    [ForeignKey("IdDepartments")]
    [InverseProperty("Employees")]
    public virtual Department? IdDepartmentsNavigation { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Employee")]
    public virtual User? IdUsersNavigation { get; set; }

    [InverseProperty("IdEmployeesNavigation")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("IdEmployeesNavigation")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
