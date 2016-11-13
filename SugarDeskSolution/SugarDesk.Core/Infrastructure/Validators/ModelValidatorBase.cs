
namespace SugarDesk.Core.Infrastructure.Validators
{
    using Base;
    using FluentValidation;
    using FluentValidation.Results;

    public class ModelValidatorBase<T> : AbstractValidator<T>, IModelValidator  where T : ViewModelBase 
    {
        T ViewModel;

        public ModelValidatorBase(T viewModel)
        {
            this.ViewModel = viewModel;
        }

        public ValidationResult Validate()
        {
            return Validate(ViewModel);
        }
    }
}
