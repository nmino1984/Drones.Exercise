using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Drones.Test;

[TestClass]
public class BatteryLogControllerTests
{
    private static HttpClient _client = null!;

    [ClassInitialize]
    public static void Init(TestContext _)
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task GetAllLogs_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/BatteryLog");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsTrue(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task GetLogsByDrone_WithAnyId_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/BatteryLog/drone/1");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsTrue(body.GetProperty("isSuccess").GetBoolean());
    }
}
