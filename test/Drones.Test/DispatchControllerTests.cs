using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Drones.Test;

[TestClass]
public class DispatchControllerTests
{
    private static HttpClient _client = null!;

    [ClassInitialize]
    public static void Init(TestContext _)
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task GetAvailableDrones_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/Dispatch/AvailableDrones");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsTrue(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task CheckDroneBattery_WithInvalidId_ReturnsOkWithFailure()
    {
        var response = await _client.GetAsync("/api/Dispatch/DroneBattery/99999");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsFalse(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task RegisterDrone_WithValidPayload_ReturnsOk()
    {
        var payload = new
        {
            serialNumber = "TEST-SN-001",
            model = 1,
            weightLimit = 200.0,
            batteryCapacity = 100.0,
            batteryLevel = 80.0,
            state = 0
        };

        var response = await _client.PostAsJsonAsync("/api/Dispatch/RegisterDrone", payload);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsTrue(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task RegisterDrone_WithMissingSerialNumber_ReturnsBadRequest()
    {
        var payload = new
        {
            serialNumber = "",
            model = 1,
            weightLimit = 200.0,
            batteryCapacity = 100.0,
            batteryLevel = 80.0,
            state = 0
        };

        var response = await _client.PostAsJsonAsync("/api/Dispatch/RegisterDrone", payload);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
