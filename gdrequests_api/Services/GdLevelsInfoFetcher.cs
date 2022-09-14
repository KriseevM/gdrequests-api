using gdrequests_api.Data.Entities;

namespace gdrequests_api.Services;

public class GdLevelsInfoFetcher
{
    private readonly HttpClient _client;
    private readonly ILogger<GdLevelsInfoFetcher> _logger;
    private readonly IConfiguration _config;

    public GdLevelsInfoFetcher(HttpClient client, ILogger<GdLevelsInfoFetcher> logger, IConfiguration config)
    {
        _client = client;
        _logger = logger;
        _config = config;
        _client.BaseAddress = new Uri(_config["GD:ServerAddress"]  /*"http://www.boomlings.com/database/"*/);
        _client.DefaultRequestHeaders.UserAgent.Clear();
    }

    public async Task<LevelMetadata> FetchAsync(int levelId)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("secret", _config["GD:RobtopSecret"]),
            new KeyValuePair<string, string>("str", levelId.ToString()),
            new KeyValuePair<string, string>("type", "19")
        });
        var response = await _client.PostAsync(_config["GD:LevelsPageName"], content);
        var responseString = await response.Content.ReadAsStringAsync();
        if (responseString == "-1" || String.IsNullOrEmpty(responseString))
        {
            throw new ArgumentException("There's no such level on Geometry Dash servers", nameof(levelId));
        }
        var hashSplitResponse = responseString.Split('|')[0].Split('#');
        var splitResponse = hashSplitResponse[0].Split(':');
        string authorName = !String.IsNullOrEmpty(hashSplitResponse[1]) ?
            hashSplitResponse[1].Split(':')[1] : "-";
        
        Dictionary<int, string> metaDictionary = new Dictionary<int, string>();
        for (int i = 0; i < splitResponse.Length; i += 2)
        {
            if (Int32.TryParse(splitResponse[i], out int key)) 
            {
                metaDictionary[key] = splitResponse[i + 1];
            }
        }
        
        LevelMetadata metadata = new LevelMetadata()
        {
            LevelName = metaDictionary[2],
            Author = authorName,
            AuthorId = Int32.Parse(metaDictionary[6]),
            Difficulty = Int32.Parse(metaDictionary[9])/10,
            IsDemon = metaDictionary.ContainsKey(17) && metaDictionary[17] == "1",
            IsAuto = metaDictionary.ContainsKey(25) && metaDictionary[25] == "1",
            Rate = Int32.Parse(metaDictionary[18]),
            IsEpic = metaDictionary[42] != "0",
            IsFeatured = metaDictionary[19] != "0"
        };
        return metadata;
    }
}