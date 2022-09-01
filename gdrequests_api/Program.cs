using gdrequests_api.Data;
using gdrequests_api.Services;
using Lib.AspNetCore.ServerSentEvents;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<GdLevelsChecker>();
builder.Services.AddScoped<GdLevelsChecker>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MainDataContext>();
builder.Services.AddServerSentEvents();
builder.Services.AddHostedService<ServerEventsWorker>();

builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<MainDataContext>().Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapServerSentEvents("/level-updates-notifier");
app.MapControllers();

app.Run();