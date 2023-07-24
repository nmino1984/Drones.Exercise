﻿using Drones.Application.ViewModels.Drone.Request;
using FluentValidation;

namespace Drones.Application.Validators.Category
{
    public class DroneValidator : AbstractValidator<DroneRequestViewModel>
    {
        public DroneValidator()
        {
            RuleFor(x => x.SerialNumber)
                .NotNull().WithMessage("Name can't be Null")
                .NotEmpty().WithMessage("Name can't be Empty");


            RuleFor(x => x.State)
                .NotNull().WithMessage("State can't be Null")
                .NotEmpty().WithMessage("State can't be Empty");


            RuleFor(x => x.WeightLimit)
                .NotNull().WithMessage("Weight Limit can't be Null")
                .NotEmpty().WithMessage("Weight Limit can't be Empty")
                .LessThan(0).WithMessage("Weight Limit can't be Less Than Zero");
        }
    }
}
