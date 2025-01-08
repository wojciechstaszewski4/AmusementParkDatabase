using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmusementParkDB.Models;

[Table("Attraction_Categories")]
public partial class AttractionCategory
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [InverseProperty("IdAttractionCategoriesNavigation")]
    public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();
}
