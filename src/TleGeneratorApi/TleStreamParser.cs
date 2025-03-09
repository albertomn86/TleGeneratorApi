namespace TleGeneratorApi;

public static class TleStreamParser
{
    private const int TITLE_LINE_LENGTH = 24;

    public static IEnumerable<TleEntry> GetTleEntries(TleGroup tleGroup, string tleData)
    {
        using StreamReader sr = new(tleData);

        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            if (line.Length == TITLE_LINE_LENGTH)
            {
                string? line1 = sr.ReadLine();
                string? line2 = sr.ReadLine();

                if (string.IsNullOrWhiteSpace(line1) || string.IsNullOrWhiteSpace(line2))
                {
                    continue;
                }

                yield return new TleEntry
                {
                    ObjectName = line.Trim(),
                    Line1 = line1,
                    Line2 = line2,
                    CatalogNumber = Convert.ToInt32(line2.Split()[1]),
                    TleGroup = tleGroup,
                    TleGroupName = tleGroup.Name
                };
            }
        }
    }
}
