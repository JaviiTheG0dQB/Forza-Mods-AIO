﻿using System.Windows;
using Forms = System.Windows.Forms;

namespace Forza_Mods_AIO.Tabs.Self_Vehicle.DropDownTabs;

/// <summary>
///     Interaction logic for CameraPage.xaml
/// </summary>
public partial class UnlocksPage
{
    public static UnlocksPage Up;
    
    public UnlocksPage()
    {
        InitializeComponent();
        Up = this;
    }
    
    private void CreditsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!CreditsSwitch.IsOn)
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.CreditsHookAddr,
                new byte[] { 0x89, 0x84, 0x24, 0x80, 0x00, 0x00, 0x00 });
            return;
        }
        
        if (Forms.MessageBox.Show("Credits Editor is a WIP, it may also mess with ur level. \nDo you wanna continue?",@"Credits Editor",Forms.MessageBoxButtons.YesNo) != Forms.DialogResult.Yes)
        {
            CreditsSwitch.IsOn = false;
            return;
        }

        ASM.Credits();
    }
    
    private void CreditsNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        try { MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.CodeCave13 + 0x35).ToString("X"), (int)CreditsNum.Value);} catch {}
    }
    
    private void XpSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        if (ASM.XpFirstTime)
            XpNum.IsEnabled = true;
        
        if (!XpSwitch.IsOn)
        {
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPaddr, new byte[] { 0xF3, 0x0F, 0x2C, 0xC6, 0x89, 0x45, 0xB8 });
            MainWindow.mw.m.WriteArrayMemory(Self_Vehicle_Addrs.XPAmountaddr, MainWindow.mw.gvp.Name == "Forza Horizon 5" ? new byte[] { 0x8B, 0x89, 0xB8, 0x00, 0x00, 0x00 } : new byte[] { 0x8B, 0x89, 0xC0, 0x00, 0x00, 0x00 });
            return;
        }
        
        if (Forms.MessageBox.Show("WARNING:\nThere have been reports of bans when using XP hacks.\nUse at your own risk. \nDo you wanna continue?",@"XP",Forms.MessageBoxButtons.YesNo) != Forms.DialogResult.Yes)
        {
            XpSwitch.IsOn = false;
            return;
        }
        
        ASM.StartXPtool();
    }

    public void XpNum_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
    {
        MainWindow.mw.m.WriteMemory((Self_Vehicle_Addrs.CodeCave3 + 0x2b).ToString("X"), (int)XpNum.Value);
    }

    private void HornUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void EmoteUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void QuickChatsUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void CosmeticUnlockerSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DiscoverRoadsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void SmashBoardsSwitch_OnToggled(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}