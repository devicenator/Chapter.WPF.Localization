// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tryout;

public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged([CallerMemberName] string property = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    protected void NotifyAndSet<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        backingField = newValue;
        NotifyPropertyChanged(propertyName);
    }

    protected void NotifyAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (!Equals(backingField, newValue))
            NotifyAndSet(ref backingField, newValue, propertyName);
    }
}