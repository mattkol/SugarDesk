// -----------------------------------------------------------------------
// <copyright file="ReadViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using Prism.Mvvm;
    using Core.Infrastructure.Converters;

    /// <summary>
    /// This class represents ReadViewModel class.
    /// </summary>
    public class ReadViewModel : BindableBase
    {

        public ReadViewModel()
        {
            GridRowType = EnumOptionType.Two;
        }

        public EnumOptionType GridRowType { get; set; }

    }
}
