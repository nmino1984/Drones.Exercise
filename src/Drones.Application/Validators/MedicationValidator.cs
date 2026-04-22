using FluentValidation;
using Drones.Application.ViewModels.Request;

namespace Drones.Application.Validators
{
    public class MedicationValidator : AbstractValidator<MedicationRequestViewModel>
    {
        public MedicationValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty")
                .Matches("^[a-zA-Z0-9-_]*$");


            RuleFor(x => x.Weight)
                .NotNull().WithMessage("Weight can't be Null")
                .GreaterThan(0).WithMessage("Weight must be greater than zero");

            RuleFor(x => x.Code)
                .NotNull().WithMessage("Code can't be Null")
                .NotEmpty().WithMessage("Code can't be Empty")
                .Matches("^[A-Z0-9_]*$");


        }
    }
}
