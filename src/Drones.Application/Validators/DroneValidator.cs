using Drones.Application.ViewModels.Drone.Request;
using Drones.Utilities.Statics;
using FluentValidation;

namespace Drones.Application.Validators
{
    public class DroneValidator : AbstractValidator<DroneRequestViewModel>
    {
        public DroneValidator()
        {
            RuleFor(x => x.SerialNumber)
                .NotNull().WithMessage("Serial Number can't be Null")
                .NotEmpty().WithMessage("Serial Number can't be Empty")
                .Length(1, 100).WithMessage("Serial Number must be between 1 and 100 characters");

            RuleFor(x => x.Model)
                .Must(m => Enum.IsDefined(typeof(ModelTypes), m))
                .WithMessage("Model must be a valid value: 1=Lightweight, 2=Middleweight, 3=Cruiserweight, 4=Heavyweight");

            RuleFor(x => x.State)
                .Must(s => Enum.IsDefined(typeof(StateTypes), s))
                .WithMessage("State must be a valid value: 0=IDLE, 1=LOADING, 2=LOADED, 3=DELIVERING, 4=DELIVERED, 5=RETURNING");

            RuleFor(x => x.WeightLimit)
                .GreaterThanOrEqualTo(0).WithMessage("Weight Limit can't be Less Than Zero")
                .LessThanOrEqualTo(500).WithMessage("Weight Limit can't be Greater Than 500");

            RuleFor(x => x.BatteryCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("Battery Capacity can't be Less Than Zero")
                .LessThanOrEqualTo(100).WithMessage("Battery Capacity can't be Greater Than 100");

            RuleFor(x => x.BatteryLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Battery Level can't be Less Than Zero")
                .LessThanOrEqualTo(100).WithMessage("Battery Level can't be Greater Than 100");
        }
    }
}
