// -----------------------------------------------------------------------
// <copyright file="AccountMessage.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Messages
{
    using Prism.Events;
    using Models;

    public class AccountMessage : PubSubEvent<SugarCrmAccount>
    {
    }
}
