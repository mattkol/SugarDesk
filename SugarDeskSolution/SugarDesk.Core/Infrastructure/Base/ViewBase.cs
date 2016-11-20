// -----------------------------------------------------------------------
// <copyright file="ViewBase.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Base
{
    using Actions;
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

                AddCommandTrigger(this.DataContext, "Loaded", "OnLoadedCommand", this);
                AddCommandTrigger(this.DataContext, "PreviewKeyDown", "OnPreviewKeyDownCommand");
                AddCommandTrigger(this.DataContext, "PreviewKeyUp", "OnPreviewKeyUpCommand");
                AddCommandTrigger(this.DataContext, "PreviewMouseWheel", "OnPreviewMouseWheelCommand");

            };

            Initialized += handler;
        }

        private void AddCommandTrigger(object viewModel, string eventName, string commandBinding, object commandParameter)
        {
            System.Windows.Interactivity.InvokeCommandAction action = new System.Windows.Interactivity.InvokeCommandAction();
            action.CommandParameter = commandParameter;
            Binding actionBinding = new Binding();
            actionBinding.Source = viewModel;
            actionBinding.Path = new PropertyPath(commandBinding);
            BindingOperations.SetBinding(action, System.Windows.Interactivity.InvokeCommandAction.CommandProperty, actionBinding);

            System.Windows.Interactivity.EventTrigger trigger = new System.Windows.Interactivity.EventTrigger();
            trigger.EventName = eventName;
            trigger.Actions.Add(action);
            trigger.Attach(this);
        }

        private void AddCommandTrigger(object viewModel, string eventName, string commandBinding)
        {
            InvokeCommandWithParamAction action = new InvokeCommandWithParamAction();
            Binding actionBinding = new Binding();
            actionBinding.Source = viewModel;
            actionBinding.Path = new PropertyPath(commandBinding);
            BindingOperations.SetBinding(action, InvokeCommandWithParamAction.CommandProperty, actionBinding);

            System.Windows.Interactivity.EventTrigger trigger = new System.Windows.Interactivity.EventTrigger();
            trigger.EventName = eventName;
            trigger.Actions.Add(action);
            trigger.Attach(this);
        }
    }
}
