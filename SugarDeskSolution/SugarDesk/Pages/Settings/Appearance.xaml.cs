// -----------------------------------------------------------------------
// <copyright file="Appearance.xaml.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Pages.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Appearance"/> class.
        /// </summary>
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            DataContext = new AppearanceViewModel();
        }
    }
}
