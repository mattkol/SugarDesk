// -----------------------------------------------------------------------
// <copyright file="AddUrlViewModel.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.ViewModels
{
    using Prism.Mvvm;

    public class AddUrlViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
