// -----------------------------------------------------------------------
// <copyright file="EnableButtonMessage.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Messages
{
    using Models;
    using Prism.Events;

    /// <summary>
    /// This class represents EnableButtonMessage class, extends Prism PubSubEvent.
    /// </summary>
    public class EnableButtonMessage : PubSubEvent<bool>
    {
    }
}
