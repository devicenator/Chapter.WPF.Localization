// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System;
using System.Globalization;

// ReSharper disable once CheckNamespace

namespace Chapter.WPF.Localization;

/// <summary>
///     The event parameter raised with the changing and changed events within the <see cref="Localizer" />.
/// </summary>
public sealed class CultureChangeEventArgs : EventArgs
{
    internal CultureChangeEventArgs(CultureInfo oldCulture, CultureInfo newCulture)
    {
        OldCulture = oldCulture;
        NewCulture = newCulture;
    }

    /// <summary>
    ///     Gets the previous culture.
    /// </summary>
    public CultureInfo OldCulture { get; }

    /// <summary>
    ///     Gets the new culture.
    /// </summary>
    public CultureInfo NewCulture { get; }
}