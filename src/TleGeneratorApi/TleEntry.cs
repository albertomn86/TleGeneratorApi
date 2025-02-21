using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TleGeneratorApi;

public class TleEntry
{
    [Key]
    public int CatalogNumber { get; set; }
    public required string ObjectName { get; set; }
    public required string Line1 { get; set; }
    public required string Line2 { get; set; }

    [ForeignKey("TleGroup")]
    public required string TleGroupName { get; set; }
    public required TleGroup TleGroup { get; set; }
}
