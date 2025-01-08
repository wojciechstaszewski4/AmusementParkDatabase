using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AmusementParkDB.Models;

[Index("Name", Name = "UQ__Departme__737584F6C5E4BB9A", IsUnique = true)]
public partial class Department
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [InverseProperty("IdDepartmentsNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
