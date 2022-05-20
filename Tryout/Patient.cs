// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

namespace Tryout;

public class Patient : ObservableObject
{
    private string _firstName;
    private string _lastName;

    public Patient(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName
    {
        get => _firstName;
        set => NotifyAndSetIfChanged(ref _firstName, value);
    }

    public string LastName
    {
        get => _lastName;
        set => NotifyAndSetIfChanged(ref _lastName, value);
    }
}