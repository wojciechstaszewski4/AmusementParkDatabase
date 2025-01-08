using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

public partial class Agreement
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int IdUsers { get; set; }

    [StringLength(50)]
    public string Type { get; set; } = null!;

    public DateOnly Date { get; set; }

    [ForeignKey("IdUsers")]
    [InverseProperty("Agreements")]
    public virtual User IdUsersNavigation { get; set; } = null!;
}
