using gdrequests_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gdrequests_api.Controllers;

public class RequestsController : Controller
{
    private ILogger<RequestsController> _logger;
    private GdLevelsChecker _levelsChecker;
    public RequestsController(ILogger<RequestsController> logger, GdLevelsChecker levelsChecker)
    {
        _logger = logger;
        _levelsChecker = levelsChecker;
    }
    
    [HttpPut("[controller].[action]")]
    public async Task<IActionResult> Add(int levelId)
    {
        _logger.Log(LogLevel.Debug, "Received {1} level. Checking", levelId);
        var levelExists = await _levelsChecker.CheckLevelExistenceAsync(levelId);
        if (levelExists)
        {
            _logger.Log(LogLevel.Debug, "Level was found on GD server");
            return StatusCode(201);
        }
        _logger.Log(LogLevel.Debug, "Level was not found on GD server");
        return StatusCode(400);
    }
}