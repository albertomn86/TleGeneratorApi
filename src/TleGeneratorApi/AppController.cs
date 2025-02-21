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
    public IActionResult GetObjecstByCatalogNumber(List<int> catalogNumbers)
    {
        if (catalogNumbers.Count == 0)
        {
            return BadRequest();
        }
       
        var tleEntries = _dbContext.TleEntries.Where(t => catalogNumbers.Contains(t.CatalogNumber)).ToList();
       
        return Ok(tleEntries);
    }
}
