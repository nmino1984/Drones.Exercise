using Drones.Application.ViewModels.Drone.Request;
using FluentValidation;
using Drones.Application.ViewModels.Request;

namespace Drones.Application.Validators.Category
{
    public class DroneMedicationValidator : AbstractValidator<DroneMedicationRequestViewModel>
    {
        public DroneMedicationValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty");


            RuleFor(x => x.State  )
                .NotNull().WithMessage("State can't be Null")
                .NotEmpty().WithMessage("State can't be Empty");

            RuleFor(x => x.WeightLimit)
                .NotNull().WithMessage("Weight Limit can't be Null")
                .NotEmpty().WithMessage("Weight Limit can't be Empty");

            RuleFor(x => x.BatteryCapacity)
                .NotNull().WithMessage("BatteryCapacity can't be Null")
                .NotEmpty().WithMessage("BatteryCapacity can't be Empty");

        }
    }
}
