using Microsoft.AspNetCore.Mvc;

namespace TleGeneratorApi;

[ApiController]
public class AppController : ControllerBase
{
    private readonly IAppDbContext _dbContext;

    public AppController(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetObjectByCatalogNumber(int catalogNumber)
    {
       var tleEntry = _dbContext.TleEntries.FirstOrDefault(e => e.CatalogNumber == catalogNumber);
       return Ok(tleEntry);
    }
}
