using Drones.Application.ViewModels.Drone.Request;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Drones.Application.Validators
{
    public class DroneValidator : AbstractValidator<DroneRequestViewModel>
    {
        public DroneValidator()
        {
            RuleFor(x => x.SerialNumber)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty")
                //.Matches(@"[a-zA-Z0-9_\-]")
                //.Matches("^[A-Za-z0-9]*$")
                .Length(1, 100);


            RuleFor(x => x.WeightLimit)
                .NotEmpty().WithMessage("Weight Limit can't be Empty")
                .LessThanOrEqualTo(500).WithMessage("Weight Limit can't be Greater Than 500")
                .GreaterThanOrEqualTo(0).WithMessage("Weight Limit can't be Less Than Zero");

            RuleFor(x => x.BatteryCapacity)
                .NotEmpty().WithMessage("Weight Limit can't be Empty")
                .LessThanOrEqualTo(100).WithMessage("Weight Limit can't be Greater Than 100")
                .GreaterThanOrEqualTo(0).WithMessage("Weight Limit can't be Less Than Zero");

        }
    }
}
