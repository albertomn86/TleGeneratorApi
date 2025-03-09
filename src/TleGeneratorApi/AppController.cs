using Microsoft.AspNetCore.Mvc;

namespace TleGeneratorApi;

[ApiController]
public class AppController : ControllerBase
{
    private readonly IAppDbContext _dbContext;
    private readonly ITleUpdater _tleUpdater;

    public AppController(IAppDbContext dbContext, ITleUpdater tleUpdater)
    {
        _dbContext = dbContext;
        _tleUpdater = tleUpdater;
    }

    [HttpGet]
    public IActionResult GetObjecstByCatalogNumber(List<int> catalogNumbers)
    {
        if (catalogNumbers.Count == 0) return BadRequest();
       
        var tleEntries = _dbContext.TleEntries.Where(t => catalogNumbers.Contains(t.CatalogNumber)).ToList();
       
        return Ok(tleEntries);
    }

    [HttpGet]
    [Route("group/{groupName}")]
    public IActionResult GetObjecstByGroupName(string? groupName)
    {
        if (string.IsNullOrEmpty(groupName)) return BadRequest();
        
        var objects = _dbContext.TleEntries
                            .Where(t => t.TleGroupName == groupName)
                            .Select(x => new ObjectDto { CatalogNumber = x.CatalogNumber , ObjectName = x.ObjectName })
                            .ToList();
       
        if (objects.Count == 0) {
            return NotFound();
        }
        
        return Ok(objects);
    }

    [HttpPost]
    public IActionResult UpdateCatalogDatabase(List<string> groupsList)
    {
        if (groupsList.Count == 0) return BadRequest();

        if (_tleUpdater.UpdateDatabase(groupsList)) {
            return Ok();
        }
        
        return UnprocessableEntity();
    }
}
