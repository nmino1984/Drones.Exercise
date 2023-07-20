using FluentValidation;
using Drones.Application.ViewModels.Request;

namespace Drones.Application.Validators.Category
{
    public class MedicationValidator : AbstractValidator<MedicationRequestViewModel>
    {
        public MedicationValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty");


            RuleFor(x => x.Weight)
                .NotNull().WithMessage("Weight can't be Null")
                .NotEmpty().WithMessage("Weight can't be Empty");

            RuleFor(x => x.Code)
                .NotNull().WithMessage("Code can't be Null")
                .NotEmpty().WithMessage("Code can't be Empty");


        }
    }
}
