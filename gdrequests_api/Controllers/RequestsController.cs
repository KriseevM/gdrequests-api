using gdrequests_api.Data;
using gdrequests_api.Data.Entities;
using gdrequests_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gdrequests_api.Controllers;

[Route("[controller].[action]")]
public class RequestsController : Controller
{
    private readonly ILogger<RequestsController> _logger;
    private readonly GdLevelsChecker _levelsChecker;
    private readonly MainDataContext _dbContext;

    public RequestsController(ILogger<RequestsController> logger, GdLevelsChecker levelsChecker,
        MainDataContext dbContext)
    {
        _logger = logger;
        _levelsChecker = levelsChecker;
        _dbContext = dbContext;
    }

    [HttpPut]
    public async Task<IActionResult> Add(int levelId)
    {
        _logger.Log(LogLevel.Debug, "Received {0} level. Checking", levelId);
        if (_dbContext.Levels.Any(p => p.ServerId == levelId))
        {
            _logger.Log(LogLevel.Information, "Level {0} was rejected - duplicate", levelId);
            return Conflict("Level already is in database");
        }
        var levelExists = await _levelsChecker.CheckLevelExistenceAsync(levelId);
        if (!levelExists)
        {
            _logger.Log(LogLevel.Debug, "Level was not found on GD server");
            return StatusCode(410, "There's no such level on Geometry Dash servers");
        }
        _logger.Log(LogLevel.Debug, "Level was found on GD server");
        _dbContext.Levels.Add(new Level() { ServerId = levelId });
        _logger.Log(LogLevel.Information, "Level {0} was added to database", levelId);
        await _dbContext.SaveChangesAsync();
        return Ok("ok");
    }

    [HttpGet]
    public IActionResult Get(
        [FromQuery] int count = 10, 
        [FromQuery] int offset = 0)
    {
        return Json(new {
            Levels = _dbContext.Levels.Skip(offset).Take(10).ToArray(),
            TotalLevelCount = _dbContext.Levels.Count()
        });
    }
}
