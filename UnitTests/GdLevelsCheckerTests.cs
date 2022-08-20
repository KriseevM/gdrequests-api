using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using gdrequests_api.Services;
using Microsoft.Extensions.Configuration;

namespace UnitTests;

public class Tests
{
    private IConfiguration _config;
    [SetUp]
    public void Setup()
    {
        ConfigurationBuilder builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        _config = builder.Build();
    }
    // Здесь берётся заведомо большой ID уровня, чтобы он точно не существовал
    // Тем не менее отсутствие такого уровня не является гарантированным
    // Это немного нарушает принцип Unit-тестов, но здесь важно проверить,
    // что функция корректно связывается с серверами GD и правильно проверяет наличие уровня.
    // Если подставить заглушку в среде тестов, то смысла от этого теста не будет
    [Test]
    public async Task NotExistingLevelTest()
    {
        HttpClient client = new HttpClient();
        GdLevelsChecker checker = new GdLevelsChecker(client, _config);
        var actual = await checker.CheckLevelExistenceAsync(99999999);
        Assert.IsFalse(actual);
    }
    // Здесь взят ID реально существующего уровня
    // Поскольку уровень уже имеет оценку, его исчезновение маловероятно, из-за чего я считаю этот ID подходящим
    [Test]
    public async Task ExistingLevelTest()
    {
        HttpClient client = new HttpClient();
        GdLevelsChecker checker = new GdLevelsChecker(client, _config);
        var actual = await checker.CheckLevelExistenceAsync(75206202);
        Assert.IsTrue(actual);
    }
}