using System.ComponentModel.DataAnnotations;

namespace TleGeneratorApi;

public class TleGroup
{
    [Key]
    public required string Name { get; set; }
    public DateTime LastUpdated { get; set; }

    public List<TleEntry> TleEntries { get; set; } = [];
}
