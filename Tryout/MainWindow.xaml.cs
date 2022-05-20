// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

using System.Collections.Generic;
using System.Windows;

namespace Tryout;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;

        Patients = new List<Patient>
        {
            new("Diane", "Selden"),
            new("Daniel", "Ivery"),
            new("Phillip", "Whitsett"),
            new("Guadalupe", "Edwards"),
            new("Millie", "Dandrea")
        };
    }

    public List<Patient> Patients { get; }

    private void SwapNames(object sender, RoutedEventArgs e)
    {
        foreach (var patient in Patients)
            (patient.FirstName, patient.LastName) = (patient.LastName, patient.FirstName);
    }
}