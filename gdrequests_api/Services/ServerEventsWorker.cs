using gdrequests_api.Controllers;
using Lib.AspNetCore.ServerSentEvents;

namespace gdrequests_api.Services;

public class ServerEventsWorker : IHostedService
{
    private readonly IServerSentEventsService _client;
    private readonly ILogger<ServerEventsWorker> _logger;

    public ServerEventsWorker(IServerSentEventsService client, ILogger<ServerEventsWorker> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            RequestsController.LevelAdded += RequestsControllerOnLevelAdded;
        });
    }

    private async void RequestsControllerOnLevelAdded(object? sender, EventArgs e)
    {
        try
        {
            var clients = _client.GetClients();
            if (!clients.Any()) return;
            await _client.SendEventAsync("update");
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"Exception{ex.Message}");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        RequestsController.LevelAdded -= RequestsControllerOnLevelAdded;
    }
}