// -----------------------------------------------------------------------
// <copyright file="DocumentReaderBehavor.cs" company="SugarDesk WPF MVVM Studio">
// Copyright (c) SugarDesk WPF MVVM Studio. All rights reserved. 
// </copyright>
// -----------------------------------------------------------------------

namespace SugarDesk.Core.Infrastructure.Behaviors
{
    using System.Windows;
    using System.Windows.Documents;

    public static class DocumentReaderBehavor
    {
        public static readonly DependencyProperty DocumentReaderProperty =
            DependencyProperty.RegisterAttached(
            "DocumentReader",
            typeof(object),
            typeof(DocumentReaderBehavor),
            new PropertyMetadata(DocumentReaderChanged)
            );

        private static void DocumentReaderChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var documentReader = d as FlowDocument;
            if (documentReader != null)
            {
                var content = e.NewValue as string;
                if (content != null) 
                {
                    var paragraph = new Paragraph();
                    paragraph.Inlines.Add(content);
                    documentReader.Blocks.Add(paragraph);
                }
            }
        }

        public static void SetDocumentReader(DependencyObject target, object value)
        {
            target.SetValue(DocumentReaderProperty, value);
        }

        public static object GetDocumentReader(DependencyObject target)
        {
            return target.GetValue(DocumentReaderProperty);
        }

    }
}
