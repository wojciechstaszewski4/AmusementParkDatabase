using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

[Table("ACL")]
public partial class Acl
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ID_Users")]
    public int IdUsers { get; set; }

    [Column("ACL")]
    [StringLength(255)]
    public string Acl1 { get; set; } = null!;

    [ForeignKey("IdUsers")]
    [InverseProperty("Acls")]
    public virtual User IdUsersNavigation { get; set; } = null!;
}
