namespace SugarDesk.Restful.Validators
{
    using Core.Infrastructure.Validators;
    using FluentValidation;
    using ViewModels;

    public class RestfulViewModelValidator : ModelValidatorBase<RestfulViewModel>
    {
        RestfulViewModel viewModel;

        public RestfulViewModelValidator(RestfulViewModel viewModel)
            : base(viewModel)
        {
            this.viewModel = viewModel;

            RuleFor(vm => vm.Username)
                .NotEmpty()
                .WithMessage("Username must be valid.");

            RuleFor(vm => vm.Password)
                .NotEmpty()
                .WithMessage("Password must be valid.");
        }

    }
}
