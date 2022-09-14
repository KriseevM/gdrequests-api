using System.Text.Json;
using gdrequests_api.Data;
using gdrequests_api.Data.Entities;
using gdrequests_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace gdrequests_api.Controllers;

[ApiController]
[Route("api/requests.[action]")]
public class RequestsController : ControllerBase
{
    private readonly ILogger<RequestsController> _logger;
    private readonly GdLevelsChecker _levelsChecker;
    private readonly GdLevelsInfoFetcher _levelInfo;
    private readonly MainDataContext _dbContext;
    public static event EventHandler? LevelAdded;

    public RequestsController(ILogger<RequestsController> logger, GdLevelsChecker levelsChecker,
        GdLevelsInfoFetcher levelInfo, MainDataContext dbContext)
    {
        _logger = logger;
        _levelsChecker = levelsChecker;
        _levelInfo = levelInfo;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] int levelId)
    {
        _logger.Log(LogLevel.Debug, "Received {0} level. Checking", levelId);
        if (_dbContext.Requests.Any(p => p.LevelId == levelId))
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
        var level = new Request() {LevelId = levelId};
        var metadata = await _levelInfo.FetchAsync(levelId);
        level.Metadata = metadata;
        metadata.Request = level;
        _dbContext.Requests.Add(level);
        await _dbContext.SaveChangesAsync();
        _logger.Log(LogLevel.Information, "Level {0} was added to database", levelId);
        LevelAdded?.Invoke(this, EventArgs.Empty);
        return Ok("ok");
    }

    [HttpGet]
    public IActionResult Get(
        [FromQuery] int count = 10,
        [FromQuery] int offset = 0)
    {
        _dbContext.LevelMetadata.Load();
        return new JsonResult(new
        {
            Levels = _dbContext.Requests.Skip(offset).Take(10).ToArray(),
            TotalLevelCount = _dbContext.Requests.Count()
        }, new JsonSerializerOptions
        {
            WriteIndented = false,
            Converters = {new RequestJsonConverter()}
        });
    }
}