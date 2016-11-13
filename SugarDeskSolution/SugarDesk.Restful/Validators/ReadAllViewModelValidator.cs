

namespace SugarDesk.Restful.Validators
{
    using Core.Infrastructure.Validators;
    using FluentValidation;
    using ViewModels;

    public class ReadAllViewModelValidator : ModelValidatorBase<ReadAllViewModel>
    {
        ReadAllViewModel viewModel;

        public ReadAllViewModelValidator(ReadAllViewModel viewModel)
            : base(viewModel)
        {
            this.viewModel = viewModel;

            RuleFor(vm => vm.ModelInfoSelected)
                .NotEmpty()
                .WithMessage("Select a valid SugarCRM model.");

            RuleFor(vm => vm.SelectSelectedFieldItem)
                .NotEmpty()
                .WithMessage("Select a valid field model.");
        }

    }
}
