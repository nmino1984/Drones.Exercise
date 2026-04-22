using AutoMapper;
using Drones.Application.Services;
using Drones.Application.Validators;
using Drones.Application.ViewModels.Drone.Request;
using Drones.Application.ViewModels.DroneMedication.Request;
using Drones.Domain.Entities;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Utilities.Statics;
using Moq;
using Xunit;

namespace Drones.Test;

public class DispatchApplicationTests
{
    private readonly Mock<IUnitOfWork> _uow;
    private readonly Mock<IDroneRepository> _droneRepo;
    private readonly Mock<IDroneMedicationRepository> _droneMedRepo;
    private readonly Mock<IMedicationRepository> _medicationRepo;
    private readonly Mock<IMapper> _mapperMock;
    private readonly DroneValidator _validator;
    private readonly DispatchApplication _sut;

    public DispatchApplicationTests()
    {
        _uow = new Mock<IUnitOfWork>();
        _droneRepo = new Mock<IDroneRepository>();
        _droneMedRepo = new Mock<IDroneMedicationRepository>();
        _medicationRepo = new Mock<IMedicationRepository>();
        _mapperMock = new Mock<IMapper>();

        _uow.Setup(u => u.Drone).Returns(_droneRepo.Object);
        _uow.Setup(u => u.DroneMedication).Returns(_droneMedRepo.Object);
        _uow.Setup(u => u.Medication).Returns(_medicationRepo.Object);

        _mapperMock.Setup(m => m.Map<TDrone>(It.IsAny<DroneRequestViewModel>()))
                   .Returns((DroneRequestViewModel r) => new TDrone
                   {
                       SerialNumber = r.SerialNumber,
                       Model = r.Model,
                       WeightLimit = r.WeightLimit,
                       BatteryCapacity = r.BatteryCapacity,
                       BatteryLevel = r.BatteryLevel,
                       State = r.State
                   });

        _validator = new DroneValidator();
        _sut = new DispatchApplication(_uow.Object, _mapperMock.Object, _validator);
    }

    // ── RegisterDrone ──────────────────────────────────────────────────────

    [Fact]
    public async Task RegisterDrone_ValidPayload_ReturnsSuccess()
    {
        var request = ValidDroneRequest();
        _droneRepo.Setup(r => r.IsPossibleToAddADrone()).ReturnsAsync(true);
        _droneRepo.Setup(r => r.RegisteAsync(It.IsAny<TDrone>())).ReturnsAsync(true);

        var result = await _sut.RegisterDrone(request);

        Assert.True(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_SAVE, result.Message);
    }

    [Fact]
    public async Task RegisterDrone_FleetHas10Drones_ReturnsFailed()
    {
        var request = ValidDroneRequest();
        _droneRepo.Setup(r => r.IsPossibleToAddADrone()).ReturnsAsync(false);

        var result = await _sut.RegisterDrone(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_ALREADY_10_DRONES_IN_FLEET, result.Message);
    }

    [Fact]
    public async Task RegisterDrone_EmptySerialNumber_ReturnsValidationError()
    {
        var request = ValidDroneRequest();
        request.SerialNumber = "";

        var result = await _sut.RegisterDrone(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_VALIDATE, result.Message);
        Assert.NotEmpty(result.Errors!);
    }

    [Fact]
    public async Task RegisterDrone_InvalidModel_ReturnsValidationError()
    {
        var request = ValidDroneRequest();
        request.Model = 99;

        var result = await _sut.RegisterDrone(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_VALIDATE, result.Message);
    }

    [Fact]
    public async Task RegisterDrone_InvalidState_ReturnsValidationError()
    {
        var request = ValidDroneRequest();
        request.State = 99;

        var result = await _sut.RegisterDrone(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_VALIDATE, result.Message);
    }

    [Fact]
    public async Task RegisterDrone_WeightLimitExceeds500_ReturnsValidationError()
    {
        var request = ValidDroneRequest();
        request.WeightLimit = 600;

        var result = await _sut.RegisterDrone(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_VALIDATE, result.Message);
    }

    // ── LoadDroneWithMedicationItems ──────────────────────────────────────

    [Fact]
    public async Task LoadDroneWithMedicationItems_ValidPayload_ReturnsSuccess()
    {
        var drone = BuildDrone(id: 1, battery: 80, weightLimit: 500, state: (int)StateTypes.IDLE);
        var medication = BuildMedication(id: 1, weight: 100);

        _droneRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(drone);
        _droneRepo.Setup(r => r.GetIfDroneAvailable(1)).ReturnsAsync(true);
        _medicationRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(medication);
        _droneMedRepo.Setup(r => r.LoadDroneWithMedicationItems(1, It.IsAny<List<int>>())).ReturnsAsync(true);
        _droneRepo.Setup(r => r.ChangeStateToDrone(1, StateTypes.LOADING)).ReturnsAsync(true);

        var request = new DispatchRequestViewModel { droneId = 1, listMedications = new List<int> { 1 } };
        var result = await _sut.LoadDroneWithMedicationItems(request);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task LoadDroneWithMedicationItems_BatteryLessThan25_ReturnsFailed()
    {
        var drone = BuildDrone(id: 2, battery: 10, weightLimit: 500, state: (int)StateTypes.IDLE);
        var medication = BuildMedication(id: 1, weight: 50);

        _droneRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(drone);
        _droneRepo.Setup(r => r.GetIfDroneAvailable(2)).ReturnsAsync(true);
        _medicationRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(medication);

        var request = new DispatchRequestViewModel { droneId = 2, listMedications = new List<int> { 1 } };
        var result = await _sut.LoadDroneWithMedicationItems(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_DRONE_BATTERY_LEVEL_LESS_THAN_25, result.Message);
    }

    [Fact]
    public async Task LoadDroneWithMedicationItems_WeightExceedsLimit_ReturnsFailed()
    {
        var drone = BuildDrone(id: 3, battery: 80, weightLimit: 50, state: (int)StateTypes.IDLE);
        var medication = BuildMedication(id: 1, weight: 200);

        _droneRepo.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(drone);
        _medicationRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(medication);

        var request = new DispatchRequestViewModel { droneId = 3, listMedications = new List<int> { 1 } };
        var result = await _sut.LoadDroneWithMedicationItems(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_TOO_HEAVY_LOAD_FOR_SELECTED_DRONE, result.Message);
    }

    [Fact]
    public async Task LoadDroneWithMedicationItems_DroneNotAvailable_ReturnsFailed()
    {
        var drone = BuildDrone(id: 4, battery: 80, weightLimit: 500, state: (int)StateTypes.LOADED);
        var medication = BuildMedication(id: 1, weight: 50);

        _droneRepo.Setup(r => r.GetByIdAsync(4)).ReturnsAsync(drone);
        _droneRepo.Setup(r => r.GetIfDroneAvailable(4)).ReturnsAsync(false);
        _medicationRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(medication);

        var request = new DispatchRequestViewModel { droneId = 4, listMedications = new List<int> { 1 } };
        var result = await _sut.LoadDroneWithMedicationItems(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_SELECTED_DRONE_NOT_AVAILABLE, result.Message);
    }

    // ── CheckingAvailableDronesForLoaded ─────────────────────────────────

    [Fact]
    public async Task CheckingAvailableDronesForLoaded_ReturnsOnlyIdleDronesWithBattery25Plus()
    {
        var drones = new List<TDrone>
        {
            BuildDrone(id: 1, battery: 90, weightLimit: 300, state: (int)StateTypes.IDLE),
            BuildDrone(id: 2, battery: 10, weightLimit: 300, state: (int)StateTypes.IDLE),  // low battery
            BuildDrone(id: 3, battery: 80, weightLimit: 300, state: (int)StateTypes.LOADED), // not idle
        };
        _droneRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(drones);

        var result = await _sut.CheckingAvailableDronesForLoaded();

        Assert.True(result.IsSuccess);
        Assert.Single(result.Data!);
        Assert.Equal(1, result.Data![0].Id);
    }

    [Fact]
    public async Task CheckingAvailableDronesForLoaded_NoDronesAvailable_ReturnsEmptyWithMessage()
    {
        _droneRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TDrone>());

        var result = await _sut.CheckingAvailableDronesForLoaded();

        Assert.True(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_NOT_DRONE_AVAILABLE, result.Message);
    }

    // ── CheckDroneBatteryLevelByDroneGiven ───────────────────────────────

    [Fact]
    public async Task CheckDroneBatteryLevelByDroneGiven_ExistingDrone_ReturnsBatteryLevel()
    {
        var drone = BuildDrone(id: 1, battery: 75, weightLimit: 300, state: (int)StateTypes.IDLE);
        _droneRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(drone);

        var result = await _sut.CheckDroneBatteryLevelByDroneGiven(1);

        Assert.True(result.IsSuccess);
        Assert.Equal(75, result.Data);
    }

    [Fact]
    public async Task CheckDroneBatteryLevelByDroneGiven_NonExistentDrone_ReturnsFailed()
    {
        _droneRepo.Setup(r => r.GetByIdAsync(9999)).ReturnsAsync((TDrone)null!);

        var result = await _sut.CheckDroneBatteryLevelByDroneGiven(9999);

        Assert.False(result.IsSuccess);
        Assert.Equal(ReplyMessages.MESSAGE_WRONG_DRONE_ID, result.Message);
    }

    // ── Helpers ───────────────────────────────────────────────────────────

    private static DroneRequestViewModel ValidDroneRequest() => new()
    {
        SerialNumber = "DRN-TEST-001",
        Model = 1,
        WeightLimit = 300,
        BatteryCapacity = 100,
        BatteryLevel = 80,
        State = 0
    };

    private static TDrone BuildDrone(int id, double battery, double weightLimit, int state) => new()
    {
        Id = id,
        SerialNumber = $"DRN-{id:D3}",
        Model = 1,
        WeightLimit = weightLimit,
        BatteryCapacity = 100,
        BatteryLevel = battery,
        State = state
    };

    private static TMedication BuildMedication(int id, double weight) => new()
    {
        Id = id,
        Name = $"Med-{id}",
        Code = $"MED_{id:D3}",
        Weight = weight
    };
}
