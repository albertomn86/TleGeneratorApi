namespace TleGeneratorApi;

public class TleUpdater : ITleUpdater
{
    private readonly IAppDbContext _context;

    public TleUpdater(IAppDbContext context)
    {
        _context = context;
    }

    public bool UpdateDatabase(List<string> groupsList)
    {
        return true;
    }
}