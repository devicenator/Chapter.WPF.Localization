// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF.Localizer;

/// <summary>
///     Formats the given translation.
/// </summary>
/// <example>
///     <code lang="XAML">
/// <![CDATA[
/// <ListBox ItemsSource="{Binding Patients}">
///     <ListBox.ItemTemplate>
///         <DataTemplate>
///             <Localizer:FormatterTextBlock Formatter="{DynamicResource NameFormatter}">
///                 <Localizer:FormatterPair Replace="{}{firstName}" With="{Binding FirstName}" />
///                 <Localizer:FormatterPair Replace="{}{lastName}" With="{Binding LastName}" />
///             </Localizer:FormatterTextBlock>
///         </DataTemplate>
///     </ListBox.ItemTemplate>
/// </ListBox>
/// ]]>
/// </code>
/// </example>
[ContentProperty(nameof(Pairs))]
public class FormatterTextBlock : TextBlock
{
    /// <summary>
    ///     The Formatter dependency property.
    /// </summary>
    public static readonly DependencyProperty FormatterProperty =
        DependencyProperty.Register("Formatter", typeof(string), typeof(FormatterTextBlock), new PropertyMetadata(OnTextChanged));

    /// <summary>
    ///     The Pairs dependency property.
    /// </summary>
    public static readonly DependencyProperty PairsProperty =
        DependencyProperty.Register("Pairs", typeof(IList), typeof(FormatterTextBlock), new PropertyMetadata(OnPairsChanged));

    /// <summary>
    ///     Creates a new FormatterTextBlock.
    /// </summary>
    public FormatterTextBlock()
    {
        Pairs = new FormatterPairCollection();
        Loaded += OnLoaded;
    }

    /// <summary>
    ///     The translation to format.
    /// </summary>
    public string Formatter
    {
        get => (string)GetValue(FormatterProperty);
        set => SetValue(FormatterProperty, value);
    }

    /// <summary>
    ///     The list of pairs.
    /// </summary>
    public IList Pairs
    {
        get => (IList)GetValue(PairsProperty);
        set => SetValue(PairsProperty, value);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateTranslation();
    }

    private static void OnPairsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (FormatterTextBlock)d;

        if (e.OldValue is INotifyCollectionChanged oldValue)
            oldValue.CollectionChanged -= control.OnPairsCollectionChanged;
        if (e.NewValue is INotifyCollectionChanged newValue)
            newValue.CollectionChanged += control.OnPairsCollectionChanged;

        control.SubScribe(control.Pairs?.Cast<FormatterPair>() ?? Array.Empty<FormatterPair>());
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (FormatterTextBlock)d;
        control.UpdateTranslation();
    }

    private void OnPairsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                SubScribe(e.NewItems?.Cast<FormatterPair>() ?? Array.Empty<FormatterPair>());
                break;
            case NotifyCollectionChangedAction.Remove:
                UnsubScribe(e.OldItems?.Cast<FormatterPair>() ?? Array.Empty<FormatterPair>());
                break;
            case NotifyCollectionChangedAction.Reset:
            case NotifyCollectionChangedAction.Replace:
                SubScribe(e.NewItems?.Cast<FormatterPair>() ?? Array.Empty<FormatterPair>());
                UnsubScribe(e.OldItems?.Cast<FormatterPair>() ?? Array.Empty<FormatterPair>());
                break;
        }

        UpdateTranslation();
    }

    private void SubScribe(IEnumerable<FormatterPair> pairs)
    {
        foreach (var pair in pairs)
            pair.DataChanged += OnItemDataChanged;
    }

    private void UnsubScribe(IEnumerable<FormatterPair> pairs)
    {
        foreach (var pair in pairs)
            pair.DataChanged -= OnItemDataChanged;
    }

    private void OnItemDataChanged(object sender, EventArgs e)
    {
        UpdateTranslation();
    }

    private void UpdateTranslation()
    {
        var pairs = Pairs?.Cast<FormatterPair>().ToList() ?? new List<FormatterPair>();
        if (pairs.Count == 0 || pairs.Any(x => x.Replace == null || x.With == null))
            return;

        var formattingPairs = pairs.Select(x => new[] { x.Replace, x.With }).SelectMany(x => x).ToArray();
        Text = Translator.Format(Formatter, formattingPairs);
    }
}