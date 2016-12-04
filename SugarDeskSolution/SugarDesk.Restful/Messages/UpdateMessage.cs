// -----------------------------------------------------------------------
// <copyright file="UpdateMessage.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Restful.Messages
{
    using Models;
    using Prism.Events;

    /// <summary>
    /// This class represents UpdateMessage class, extends Prism PubSubEvent.
    /// </summary>
    public class UpdateMessage : PubSubEvent<bool>
    {
    }
}
