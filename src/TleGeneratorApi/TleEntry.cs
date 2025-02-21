using System.ComponentModel.DataAnnotations;

namespace TlegeneratorApi;

public class TleEntry
{
    [Key]
    public int CatalogNumber { get; set; }
    public required string ObjectName { get; set; }
    public required string GroupName { get; set; }
    public required string Line1 { get; set; }
    public required string Line2 { get; set; }
    public DateTime LastUpdate { get; set; }
}