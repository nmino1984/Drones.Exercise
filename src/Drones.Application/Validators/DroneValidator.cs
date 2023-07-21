using Drones.Application.ViewModels.Drone.Request;
using FluentValidation;

namespace Drones.Application.Validators.Category
{
    public class DroneValidator : AbstractValidator<DroneRequestViewModel>
    {
        public DroneValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty");


            RuleFor(x => x.State  )
                .NotNull().WithMessage("State can't be Null")
                .NotEmpty().WithMessage("State can't be Empty");
        }
    }
}
