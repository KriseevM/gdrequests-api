using gdrequests_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace gdrequests_api.Controllers;

[ApiController]
[Route("api/gdserver.[action]")]
public class GdServerController : ControllerBase
{
    private readonly GdLevelsChecker _checker;

    public GdServerController(GdLevelsChecker checker)
    {
        _checker = checker;
    }
    [HttpGet]
    public async Task<bool> CheckLevel(int levelId)
    {
        bool result = await _checker.CheckLevelExistenceAsync(levelId);
        return result;
    }
}