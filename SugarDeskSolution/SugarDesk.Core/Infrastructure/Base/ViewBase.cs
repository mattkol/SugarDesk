// -----------------------------------------------------------------------
// <copyright file="ViewBase.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Base
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ViewBase : UserControl
    {
        public ViewBase()
        {
            EventHandler handler = null;
            handler = (sender, args) =>
            {
                Initialized -= handler;
                System.Windows.Interactivity.InvokeCommandAction action = new System.Windows.Interactivity.InvokeCommandAction();
                Binding actionBinding = new Binding();
                actionBinding.Source = this.DataContext;
                actionBinding.Path = new PropertyPath("OnLoadedCommand");
                BindingOperations.SetBinding(action, System.Windows.Interactivity.InvokeCommandAction.CommandProperty, actionBinding);

                System.Windows.Interactivity.EventTrigger trigger = new System.Windows.Interactivity.EventTrigger();
                trigger.EventName = "Loaded";
                trigger.Actions.Add(action);
                trigger.Attach(this);
            };

            Initialized += handler;
        }
    }
}
