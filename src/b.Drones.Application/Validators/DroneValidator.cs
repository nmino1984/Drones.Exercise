using FluentValidation;
using Drones.Application.ViewModels.Request;

namespace Drones.Application.Validators.Category
{
    public class DroneValidator : AbstractValidator<DroneRequestViewModel>
    {
        public DroneValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty");


            RuleFor(x => x.Description  )
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty");


        }
    }
}
