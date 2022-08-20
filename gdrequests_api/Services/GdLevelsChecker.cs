namespace gdrequests_api.Services;

public class GdLevelsChecker
{
    private HttpClient _client;
    private IConfiguration _config;
    private static string RobTopSecret = "Wmfd2893gb7";
    public GdLevelsChecker(HttpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
        _client.BaseAddress = new Uri(_config["GD:ServerAddress"]  /*"http://www.boomlings.com/database/"*/);
        _client.DefaultRequestHeaders.UserAgent.Clear();
    }

    public async Task<bool> CheckLevelExistenceAsync(int levelId)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("secret", _config["GD:RobtopSecret"]),
            new KeyValuePair<string, string>("str", levelId.ToString()),
            new KeyValuePair<string, string>("type", "19")
        });
        var response = await _client.PostAsync(_config["GD:LevelsPageName"], content);
        var responseString = await response.Content.ReadAsStringAsync();
        return responseString != "-1";
    }
}