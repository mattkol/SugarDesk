namespace SugarDesk.Core.Infrastructure.Validators
{
    using Prism.Mvvm;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationErrorsContainer<T> : ErrorsContainer<T>
    {
        public ValidationErrorsContainer(Action<string> raiseErrorsChanged) : base(raiseErrorsChanged)
        {
        }

        public List<string> Keys
        {
            get
            {
                if (HasErrors)
                {
                    return validationResults.Keys.ToList();
                }

                return null;
            }
        }

        public void ClearAllErrors()
        {
            if (HasErrors)
            {
                List<string> propertyNames = Keys;
                if ((propertyNames != null) && (propertyNames.Count > 0))
                {
                    foreach (var propertyName in propertyNames)
                    {
                        ClearErrors(propertyName);
                    }
                }
            }
        }
    }
}
