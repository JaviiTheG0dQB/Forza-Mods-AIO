﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Forza_Mods_AIO.Resources;

namespace Forza_Mods_AIO.Tabs.Tuning;

public partial class Tuning
{
    public static Tuning T { get; private set; } = null!;
    public readonly UiManager UiManager;

    public Tuning()
    {
        InitializeComponent();
        T = this;
        UiManager = new UiManager(this, AobProgressBar, Sizes, IsClicked);
        UiManager.ToggleUiElements(false);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!MainWindow.Mw.Attached)
        {
            return;
        }

        Task.Run(TuningAddresses.Scan);
        ScanButton.IsEnabled = false;
    }

    #region Interaction
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        UiManager.ToggleDropDown(sender);
    }
        
    private static readonly Dictionary<string, double> Sizes = new()
    {
        { "TiresButton", 240 }, // Button name for page, height of page
        { "GearingButton", 250 },
        { "AlignmentButton", 125 },
        { "SpringsButton", 340 },
        { "DampingButton", 340 },
        { "AeroButton", 125 },
        { "SteeringButton", 340 },
        { "OthersButton", 285 },
    };

    private static readonly Dictionary<string, bool> IsClicked = new()
    {
        {"TiresButton", false },
        {"GearingButton", false },
        {"AlignmentButton", false },
        {"SpringsButton", false },
        {"DampingButton", false },
        {"AeroButton", false },
        {"SteeringButton", false },
        {"OthersButton", false }
    };
    #endregion
}