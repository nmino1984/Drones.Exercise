using Drones.Application.Validators;
using Drones.Application.ViewModels.Request;
using Xunit;

namespace Drones.Test;

public class MedicationValidatorTests
{
    private readonly MedicationValidator _validator = new();

    [Fact]
    public async Task MedicationValidator_ValidPayload_PassesValidation()
    {
        var vm = new MedicationRequestViewModel
        {
            Name = "Amoxicillin-500",
            Weight = 120.5,
            Code = "AMX_500",
            Image = "img.png"
        };

        var result = await _validator.ValidateAsync(vm);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Invalid Name!")]
    [InlineData("has spaces")]
    [InlineData("special@char")]
    public async Task MedicationValidator_InvalidName_FailsValidation(string name)
    {
        var vm = new MedicationRequestViewModel { Name = name, Weight = 10, Code = "COD_001" };

        var result = await _validator.ValidateAsync(vm);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(vm.Name));
    }

    [Fact]
    public async Task MedicationValidator_NullName_FailsValidation()
    {
        var vm = new MedicationRequestViewModel { Name = null, Weight = 10, Code = "COD_001" };

        var result = await _validator.ValidateAsync(vm);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(vm.Name));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public async Task MedicationValidator_WeightZeroOrNegative_FailsValidation(double weight)
    {
        var vm = new MedicationRequestViewModel { Name = "ValidName", Weight = weight, Code = "COD_001" };

        var result = await _validator.ValidateAsync(vm);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(vm.Weight));
    }

    [Fact]
    public async Task MedicationValidator_NullWeight_FailsValidation()
    {
        var vm = new MedicationRequestViewModel { Name = "ValidName", Weight = null, Code = "COD_001" };

        var result = await _validator.ValidateAsync(vm);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(vm.Weight));
    }

    [Theory]
    [InlineData("invalid code")]
    [InlineData("lowercase")]
    [InlineData("has-dash")]
    public async Task MedicationValidator_InvalidCode_FailsValidation(string code)
    {
        var vm = new MedicationRequestViewModel { Name = "ValidName", Weight = 10, Code = code };

        var result = await _validator.ValidateAsync(vm);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(vm.Code));
    }
}
