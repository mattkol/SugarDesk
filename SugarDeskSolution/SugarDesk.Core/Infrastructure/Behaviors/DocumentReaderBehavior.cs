// -----------------------------------------------------------------------
// <copyright file="DocumentReaderBehavior.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Behaviors
{
    using System.Windows;
    using System.Windows.Documents;

    /// <summary>
    /// This class represents DocumentReaderBehavior class.
    /// </summary>
    public static class DocumentReaderBehavior
    {
        /// <summary>
        /// Create the document reader attached property.
        /// </summary>
        public static readonly DependencyProperty DocumentReaderProperty =
                                                DependencyProperty.RegisterAttached(
                                                "DocumentReader",
                                                typeof(object),
                                                typeof(DocumentReaderBehavior),
                                                new PropertyMetadata(DocumentReaderChanged));

        /// <summary>
        /// Set the document reader object.
        /// </summary>
        /// <param name="target">DependencyObject target object</param>
        /// <param name="value">New document object to set.</param>
        public static void SetDocumentReader(DependencyObject target, object value)
        {
            target.SetValue(DocumentReaderProperty, value);
        }

        /// <summary>
        /// Get the document reader object.
        /// </summary>
        /// <param name="target">DependencyObject target object</param>
        /// <returns>Document reader object</returns>
        public static object GetDocumentReader(DependencyObject target)
        {
            return target.GetValue(DocumentReaderProperty);
        }

        /// <summary>
        /// The document reader changed function.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject object</param>
        /// <param name="eventArgs">DependencyPropertyChangedEventArgs argument</param>
        private static void DocumentReaderChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            var documentReader = dependencyObject as FlowDocument;
            if (documentReader != null)
            {
                var content = eventArgs.NewValue as string;
                if (content != null)
                {
                    var paragraph = new Paragraph();
                    paragraph.Inlines.Add(content);
                    documentReader.Blocks.Add(paragraph);
                }
            }
        }
    }
}
