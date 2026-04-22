using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Drones.Test;

[TestClass]
public class MedicationControllerTests
{
    private static HttpClient _client = null!;

    [ClassInitialize]
    public static void Init(TestContext _)
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [TestMethod]
    public async Task ListMedications_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/Medication");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsTrue(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task GetMedicationById_WithInvalidId_ReturnsOkWithFailure()
    {
        var response = await _client.GetAsync("/api/Medication/99999");

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsFalse(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task RegisterMedication_WithValidPayload_ReturnsOk()
    {
        var payload = new
        {
            name = "Aspirin-X1",
            weight = 50.5,
            code = "ASP_001",
            image = "aspirin.png"
        };

        var response = await _client.PostAsJsonAsync("/api/Medication/Register", payload);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.IsTrue(body.GetProperty("isSuccess").GetBoolean());
    }

    [TestMethod]
    public async Task RegisterMedication_WithInvalidCode_ReturnsBadRequest()
    {
        var payload = new
        {
            name = "test-med",
            weight = 10.0,
            code = "invalid code!",
            image = ""
        };

        var response = await _client.PostAsJsonAsync("/api/Medication/Register", payload);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
