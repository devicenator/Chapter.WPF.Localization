﻿// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF.Localization;

/// <summary>
///     Represents a replace pair within the <see cref="FormatterTextBlock" />.
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
public class FormatterPair : Freezable
{
    /// <summary>
    ///     The Replace dependency property.
    /// </summary>
    public static readonly DependencyProperty ReplaceProperty =
        DependencyProperty.Register("Replace", typeof(string), typeof(FormatterPair), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     The With dependency property.
    /// </summary>
    public static readonly DependencyProperty WithProperty =
        DependencyProperty.Register("With", typeof(string), typeof(FormatterPair), new PropertyMetadata(OnDataChanged));

    /// <summary>
    ///     The value to search for inside the formatter.
    /// </summary>
    public string Replace
    {
        get => (string)GetValue(ReplaceProperty);
        set => SetValue(ReplaceProperty, value);
    }

    /// <summary>
    ///     The replace with value.
    /// </summary>
    public string With
    {
        get => (string)GetValue(WithProperty);
        set => SetValue(WithProperty, value);
    }

    /// <summary>
    ///     Raised if the <see cref="Replace" /> or <see cref="With" /> changed.
    /// </summary>
    public event EventHandler DataChanged;

    private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (FormatterPair)d;
        control.DataChanged?.Invoke(control, EventArgs.Empty);
    }

    /// <inheritdoc />
    protected override Freezable CreateInstanceCore()
    {
        return this;
    }
}