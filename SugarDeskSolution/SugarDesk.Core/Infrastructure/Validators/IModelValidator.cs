// -----------------------------------------------------------------------
// <copyright file="IModelValidator.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Validators
{
    using FluentValidation.Results;

    public interface IModelValidator
    {
        ValidationResult Validate();
    }
}
