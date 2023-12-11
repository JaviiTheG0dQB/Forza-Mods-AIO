﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Forza_Mods_AIO.Resources;
using MahApps.Metro.Controls;
using static Forza_Mods_AIO.MainWindow;
using static Forza_Mods_AIO.Tabs.AutoShowTab.AutoshowVars;

namespace Forza_Mods_AIO.Tabs.AutoShowTab;

/// <summary>
///     Interaction logic for AutoShow.xaml
/// </summary>
public partial class AutoShow
{
    #region Variables

    public static AutoShow As { get; private set; } = null!;
    public readonly UiManager UiManager;
    private string _clearGarageString = "DELETE FROM Profile0_Career_Garage WHERE Id > 0;";
    private const string FreePerfString = "UPDATE List_UpgradeAntiSwayFront SET price=0;UPDATE List_UpgradeAntiSwayRear SET price=0;UPDATE List_UpgradeBrakes SET price=0;UPDATE List_UpgradeCarBodyChassisStiffness SET price=0;UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyTireWidthFront SET price=0;UPDATE List_UpgradeCarBodyTireWidthRear SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingFront SET price=0;UPDATE List_UpgradeCarBodyTrackSpacingRear SET price=0;UPDATE List_UpgradeCarBodyWeight SET price=0;UPDATE List_UpgradeDrivetrain SET price=0;UPDATE List_UpgradeDrivetrainClutch SET price=0;UPDATE List_UpgradeDrivetrainDifferential  SET price=0;UPDATE List_UpgradeDrivetrainDriveline SET price=0;UPDATE List_UpgradeDrivetrainTransmission SET price=0;UPDATE List_UpgradeEngine SET price=0;UPDATE List_UpgradeEngineCamshaft SET price=0;UPDATE List_UpgradeEngineCSC SET price=0;UPDATE List_UpgradeEngineDisplacement SET price=0;UPDATE List_UpgradeEngineDSC SET price=0;UPDATE List_UpgradeEngineExhaust SET price=0;UPDATE List_UpgradeEngineFlywheel SET price=0;UPDATE List_UpgradeEngineFuelSystem SET price=0;UPDATE List_UpgradeEngineIgnition SET price=0;UPDATE List_UpgradeEngineIntake SET price=0;UPDATE List_UpgradeEngineIntercooler SET price=0;UPDATE List_UpgradeEngineManifold SET price=0;UPDATE List_UpgradeEngineOilCooling SET price=0;UPDATE List_UpgradeEnginePistonsCompression SET price=0;UPDATE List_UpgradeEngineRestrictorPlate SET price=0;UPDATE List_UpgradeEngineTurboQuad SET price=0;UPDATE List_UpgradeEngineTurboSingle SET price=0;UPDATE List_UpgradeEngineTurboTwin SET price=0;UPDATE List_UpgradeEngineValves SET price=0;UPDATE List_UpgradeMotor SET price=0;UPDATE List_UpgradeMotorParts SET price=0;UPDATE List_UpgradeSpringDamper SET price=0;UPDATE List_UpgradeTireCompound SET price=0;UPDATE List_Wheels SET price=1;";
    private const string FreeVisualString = "UPDATE List_UpgradeCarBody SET price=0;UPDATE List_UpgradeCarBodyFrontBumper SET price=0;UPDATE List_UpgradeCarBodyHood SET price=0;UPDATE List_UpgradeCarBodyRearBumper SET price=0;UPDATE List_UpgradeCarBodySideSkirt SET price=0;UPDATE List_UpgradeRearWing SET price=0;UPDATE List_Wheels SET price=1;";
    private const string ShowTrafficHsNullString = "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL";
    private const string FixThumbnailsString = "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage; UPDATE Profile0_Career_Garage SET NumOwners=69";
    private const string ClearTagString = "UPDATE Profile0_Career_Garage SET HasCurrentOwnerViewedCar = 1;";
    private const string UnlockPresetsString = "UPDATE UpgradePresetPackages SET Purchasable = 1 WHERE Purchasable = 0";

    private const string Sql11EraseString = "                                                                                                                                                                                                                                                                                                                                                                           ";
    private const string Sql12EraseString = "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ";
    private const string Sql11OriginalString = "UPDATE %s SET TopSpeed=%f, DistanceDriven=%u, TimeDriven=%u, TotalWinnings=%u, TotalRepairs=%u, NumPodiums=%u, NumVictories=%u, NumRaces=%u, NumOwners=%u, NumTimesSold=%u, TimeDrivenInRoadTrips=%u, CurOwnerNumRaces=%u, CurOwnerWinnings=%u, NumSkillPointsEarned=%u, HighestSkillScore=%u, HasCurrentOwnerViewedCar=%u WHERE Id=%u                                     ";
    public static readonly byte[] Sql12OriginalBytes = { 0x55, 0x50, 0x44, 0x41, 0x54, 0x45, 0x20, 0x25, 0x73, 0x43, 0x61, 0x72, 0x65, 0x65, 0x72, 0x5F, 0x47, 0x61, 0x72, 0x61, 0x67, 0x65, 0x20, 0x53, 0x45, 0x54, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x61, 0x6C, 0x44, 0x72, 0x69, 0x76, 0x65, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x72, 0x73, 0x74, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x68, 0x69, 0x72, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x6F, 0x75, 0x72, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x66, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x69, 0x78, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x76, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x65, 0x69, 0x67, 0x68, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x6E, 0x69, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x42, 0x61, 0x6C, 0x61, 0x6E, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x63, 0x65, 0x6E, 0x74, 0x65, 0x72, 0x54, 0x6F, 0x72, 0x71, 0x75, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x73, 0x74, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x25, 0x31, 0x2E, 0x38, 0x65, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73, 0x69, 0x6F, 0x6E, 0x65, 0x64, 0x54, 0x75, 0x6E, 0x65, 0x49, 0x64, 0x20, 0x3D, 0x20, 0x27, 0x25, 0x73, 0x27, 0x20, 0x57, 0x48, 0x45, 0x52, 0x45, 0x20, 0x49, 0x64, 0x20, 0x3D, 0x20, 0x25, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0x50, 0x44, 0x41, 0x54, 0x45, 0x20, 0x25, 0x73, 0x43, 0x61, 0x72, 0x65, 0x65, 0x72, 0x5F, 0x47, 0x61, 0x72, 0x61, 0x67, 0x65, 0x20, 0x53, 0x45, 0x54, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x44, 0x6F, 0x77, 0x6E, 0x66, 0x6F, 0x72, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x6E, 0x61, 0x6C, 0x44, 0x72, 0x69, 0x76, 0x65, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x72, 0x73, 0x74, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x63, 0x6F, 0x6E, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x68, 0x69, 0x72, 0x64, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x6F, 0x75, 0x72, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x69, 0x66, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x69, 0x78, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x73, 0x65, 0x76, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x65, 0x69, 0x67, 0x68, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x6E, 0x69, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x74, 0x65, 0x6E, 0x74, 0x68, 0x47, 0x65, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x42, 0x61, 0x6C, 0x61, 0x6E, 0x63, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x62, 0x72, 0x61, 0x6B, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x63, 0x65, 0x6E, 0x74, 0x65, 0x72, 0x54, 0x6F, 0x72, 0x71, 0x75, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x43, 0x61, 0x73, 0x74, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x52, 0x69, 0x64, 0x65, 0x48, 0x65, 0x69, 0x67, 0x68, 0x74, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x61, 0x6D, 0x70, 0x69, 0x6E, 0x67, 0x53, 0x74, 0x69, 0x66, 0x66, 0x6E, 0x65, 0x73, 0x73, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x42, 0x75, 0x6D, 0x70, 0x52, 0x61, 0x74, 0x69, 0x6F, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x41, 0x63, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x66, 0x72, 0x6F, 0x6E, 0x74, 0x44, 0x65, 0x63, 0x65, 0x6C, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x69, 0x72, 0x65, 0x50, 0x72, 0x65, 0x73, 0x73, 0x75, 0x72, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x43, 0x61, 0x6D, 0x62, 0x65, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x54, 0x6F, 0x65, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x70, 0x72, 0x69, 0x6E, 0x67, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E, 0x67, 0x5F, 0x72, 0x65, 0x61, 0x72, 0x53, 0x77, 0x61, 0x79, 0x62, 0x61, 0x72, 0x20, 0x3D, 0x20, 0x2D, 0x31, 0x2E, 0x30, 0x2C, 0x20, 0x54, 0x75, 0x6E, 0x69, 0x6E };
    
    private readonly List<ToggleSwitch> _toggleSwitchList = new();
    #endregion
    
    public AutoShow()
    {
        InitializeComponent();
        As = this;
        
        UiManager = new UiManager(this, AobProgressBar);
        UiManager.ToggleUiElements(false);
        
        _toggleSwitchList.Add(UnlockHiddenPresets);
        _toggleSwitchList.Add(UnlockHiddenDecals);
        _toggleSwitchList.Add(FixThumbnails);
        _toggleSwitchList.Add(ShowTrafficHsNull);
        _toggleSwitchList.Add(ClearGarage);
        _toggleSwitchList.Add(ClearTag);
        _toggleSwitchList.Add(ToggleFreeCars);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        Task.Run(() => Scan());
        ScanButton.IsEnabled = false;
    }

    private void ToggleButtons(ToggleSwitch sender, bool on)
    {
        if (Mw.Gvp.Name != "Forza Horizon 4")
        {
            return;
        }

        foreach (var toggleSwitch in _toggleSwitchList.Where(toggleSwitch => on || toggleSwitch != sender))
        {
            toggleSwitch.IsEnabled = on;
        }
    }

    private void AllCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleRareCars.IsEnabled = !ToggleRareCars.IsEnabled;
        switch (ToggleAllCars.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, AllCars_OnToggled, "CREATE TABLE AutoshowTable(Id INT, NotAvailableInAutoshow INT); INSERT INTO AutoshowTable SELECT Id, NotAvailableInAutoshow FROM Data_Car; UPDATE Data_Car SET NotAvailableInAutoshow = 0;");
                break;
            }
            case true when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql5, "                            ");
                Mw.M.WriteStringMemory(Sql3, "                  ");
                Mw.M.WriteStringMemory(Sql7, "                                           ");
                Mw.M.WriteStringMemory(Sql1, "    Garage.IsInstalled            AS PurchasableCar,");
                Mw.M.WriteStringMemory(Sql9, "                                    ");
                Mw.M.WriteStringMemory(Sql8, "           1215=");
                Mw.M.WriteStringMemory(Sql4, "      1215=");
                
                if (Sql16 == 0)
                {
                    return;
                }

                Mw.M.WriteStringMemory(Sql16, "                     ");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, AllCars_OnToggled, "UPDATE Data_Car SET NotAvailableInAutoshow = (SELECT NotAvailableInAutoshow FROM AutoshowTable WHERE Data_Car.Id == AutoshowTable.Id); DROP TABLE AutoshowTable;");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql5, "AND NotAvailableInAutoshow=0");
                Mw.M.WriteStringMemory(Sql3, "AND NOT IsBarnFind");
                Mw.M.WriteStringMemory(Sql7, "AND IsCarVisibleAndReleased(Garage.ModelId)"); 
                Mw.M.WriteStringMemory(Sql1, "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                Mw.M.WriteStringMemory(Sql9, "AND UnobtainableCars.Ordinal IS NULL");
                Mw.M.WriteStringMemory(Sql8, "Garage.ModelId!=");
                Mw.M.WriteStringMemory(Sql4, "Garage.Id!=");

                if (Sql16 == 0)
                {
                    return;
                }
                
                Mw.M.WriteStringMemory(Sql16,  "AND NOT IsMidnightCar"); 
                break;
            }
        }
    }

    private void RareCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleAllCars.IsEnabled = !ToggleAllCars.IsEnabled;
            
        switch (ToggleRareCars.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, RareCars_OnToggled, "CREATE TABLE AutoshowTable(Id INT, NotAvailableInAutoshow INT); INSERT INTO AutoshowTable SELECT Id, NotAvailableInAutoshow FROM Data_Car; UPDATE Data_Car SET NotAvailableInAutoshow = CASE WHEN 1 THEN 0 WHEN 0 THEN 1 END;");
                break;
            }
            case true when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql6, "=1                                    ");
                Mw.M.WriteStringMemory(Sql3, "                  ");
                Mw.M.WriteStringMemory(Sql1, "    Garage.IsInstalled            AS PurchasableCar,");
                Mw.M.WriteStringMemory(Sql9, "                                    ");
                Mw.M.WriteStringMemory(Sql8, "           1215=");
                Mw.M.WriteStringMemory(Sql4, "      1215=");
               
                if (Sql16 == 0)
                {
                    return;
                }
                
                Mw.M.WriteStringMemory(Sql16, "                     ");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, RareCars_OnToggled, "UPDATE Data_Car SET NotAvailableInAutoshow = (SELECT NotAvailableInAutoshow FROM AutoshowTable WHERE Data_Car.Id == AutoshowTable.Id); DROP TABLE AutoshowTable;");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql6, "=0                                    ");
                Mw.M.WriteStringMemory(Sql3, "AND NOT IsBarnFind");
                Mw.M.WriteStringMemory(Sql1, "NOT Garage.NotAvailableInAutoshow AS PurchasableCar,");
                Mw.M.WriteStringMemory(Sql9, "AND UnobtainableCars.Ordinal IS NULL");
                Mw.M.WriteStringMemory(Sql8, "Garage.ModelId!=");
                Mw.M.WriteStringMemory(Sql4, "Garage.Id!=");

                if (Sql16 == 0)
                {
                    return;
                }
                
                Mw.M.WriteStringMemory(Sql16, "AND NOT IsMidnightCar"); 
                break;
            }
        }
    }

    private void FreeCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender));
            
        switch (ToggleFreeCars.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, FreeCars_OnToggled ,"CREATE TABLE CostTable(Id INT, BaseCost INT); INSERT INTO CostTable(Id, BaseCost) SELECT Id, BaseCost FROM Data_car; UPDATE Data_Car SET BaseCost = 0;");
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql11,"UPDATE Data_Car SET BaseCost = 0 WHERE BaseCost >0                                                                                                                                                                                                                                                                                                                         ");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, FreeCars_OnToggled ,"UPDATE Data_Car SET BaseCost = (SELECT BaseCost FROM CostTable WHERE Id = Data_Car.Id); DROP TABLE CostTable;");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, Sql11OriginalString);
                break;
            }
        }
    }

    private void PaintLegoCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        switch (PaintLegoCars.IsOn)
        {
            case true:
            {
                Mw.M.WriteStringMemory(Sql14, "b");
                Mw.M.WriteStringMemory(Sql15, "b");
                break;
            }
            case false:
            {
                Mw.M.WriteStringMemory(Sql14, "H");
                Mw.M.WriteStringMemory(Sql15, "H");
                break;
            }
        }
    }


    private void RemoveAnyCar_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        if (RemoveAnyCar.IsOn)
        {
            Mw.M.WriteStringMemory(Sql10, "b");
            Mw.M.WriteStringMemory(Sql2, "b");
        }
        else
        {
            Mw.M.WriteStringMemory(Sql10, "D");
            Mw.M.WriteStringMemory(Sql2, "I");
        }
    }

    private void ClearGarage_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender));
            
        switch (ClearGarage.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, ClearGarage_OnToggled, _clearGarageString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql11, Sql11EraseString);
                Mw.M.WriteStringMemory(Sql11, _clearGarageString);
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, Sql11OriginalString);
                break;
            }
        }
    }

    private void FixThumbnails_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender));
            
        switch (FixThumbnails.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, FixThumbnails_OnToggled, "UPDATE Profile0_Career_Garage SET Thumbnail=(SELECT Thumbnail FROM Data_Car WHERE Data_Car.Id = Profile0_Career_Garage.CarId); UPDATE Profile0_Career_Garage; UPDATE Profile0_Career_Garage SET NumOwners=69");
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql11, FixThumbnailsString);
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11,Sql11OriginalString);
                break;
            }
        }
    }

    private void AddRareCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var addCarsString = "INSERT INTO ContentOffersMapping (OfferId, ContentId, ContentType, IsPromo, IsAutoRedeem, ReleaseDateUTC, Quantity) SELECT 3, Id, 1, 0, 1, NULL, 1 FROM Data_Car WHERE Id NOT IN (SELECT ContentId AS Id FROM ContentOffersMapping WHERE ContentId IS NOT NULL);" +
                            " INSERT INTO Profile0_FreeCars SELECT Id, 1 FROM Data_Car WHERE Id NOT IN (SELECT CarId AS Id FROM Profile0_FreeCars WHERE CarID IS NOT NULL);" +
                            " UPDATE ContentOffersMapping SET Quantity = 9999 ;" +
                            " UPDATE Profile0_FreeCars SET FreeCount = 1;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 1;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(3300);" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(SELECT Id AS ContentId FROM Data_Car WHERE NotAvailableInAutoshow = 0);" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(SELECT ContentId FROM ContentOffersMapping WHERE ReleaseDateUTC > '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00' OR ReleaseDateUTC IS NULL)" ;
            
        switch (QuickAddRareCars.IsEnabled)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, AddRareCars_OnToggled, addCarsString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql12, Sql12EraseString);
                Mw.M.WriteArrayMemory(Sql13, new byte[] { 0x00 });
                Mw.M.WriteStringMemory(Sql12, addCarsString);
                QuickAddAllCars.IsEnabled = false;
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteArrayMemory(Sql12, Sql12OriginalBytes);
                QuickAddAllCars.IsEnabled = true;
                break;
            }
        }
    }

    private void AddAllCars_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        var addCarsString = "INSERT INTO ContentOffersMapping (OfferId, ContentId, ContentType, IsPromo, IsAutoRedeem, ReleaseDateUTC, Quantity) SELECT 3, Id, 1, 0, 1, NULL, 1 FROM Data_Car WHERE Id NOT IN (SELECT ContentId AS Id FROM ContentOffersMapping WHERE ContentId IS NOT NULL);" +
                            " INSERT INTO Profile0_FreeCars SELECT ContentId, 1 FROM ContentOffersMapping;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 1 WHERE ContentId NOT IN(SELECT ContentId FROM ContentOffersMapping WHERE ReleaseDateUTC > '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00' OR ReleaseDateUTC IS NULL)" +
                            " UPDATE ContentOffersMapping SET Quantity = 1;" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(3300);" +
                            " UPDATE ContentOffersMapping SET IsAutoRedeem = 0 WHERE ContentId IN(SELECT CarId AS ContentId FROM Profile0_Career_Garage WHERE CarId IS NOT NULL);";
            
        switch (QuickAddAllCars.IsEnabled)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, AddAllCars_OnToggled, addCarsString);
                break;
            }
            case true when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql12, Sql12EraseString);
                Mw.M.WriteArrayMemory(Sql13, new byte[] { 0x00 });
                Mw.M.WriteStringMemory(Sql12, addCarsString);
                QuickAddRareCars.IsEnabled = false;
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteArrayMemory(Sql12, Sql12OriginalBytes);
                QuickAddRareCars.IsEnabled = true;
                break;
            }
        }
    }

    private void FreeVisualUpgrades_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        switch (FreeVisualUpgrades.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender,FreeVisualUpgrades_OnToggled, FreeVisualString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql12, Sql12EraseString);
                Mw.M.WriteArrayMemory(Sql13, new byte[] { 0x00 });
                Mw.M.WriteStringMemory(Sql12, FreeVisualString);
                FreePerfUpgrades.IsEnabled = false;
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteArrayMemory(Sql12, Sql12OriginalBytes);
                FreePerfUpgrades.IsEnabled = true;
                break;
            }
        }
    }

    private void FreePerfUpgrades_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        switch (FreePerfUpgrades.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, FreePerfUpgrades_OnToggled, FreePerfString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql12, Sql12EraseString);
                Mw.M.WriteArrayMemory(Sql13, new byte[] { 0x00 });
                Mw.M.WriteStringMemory(Sql12, FreePerfString);
                FreeVisualUpgrades.IsEnabled = false;
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteArrayMemory(Sql12, Sql12OriginalBytes);
                FreeVisualUpgrades.IsEnabled = true;
                break;
            }
        }
    }

    private void ShowTrafficHSNull_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender)!);
            
        switch (ShowTrafficHsNull.IsEnabled)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, ShowTrafficHSNull_OnToggled, ShowTrafficHsNullString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql11, Sql11EraseString);
                Mw.M.WriteStringMemory(Sql11, ShowTrafficHsNullString);
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, Sql11OriginalString);
                break;
            }
        }
    }

    private void UnlockHiddenDecals_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender)!);
            
        switch (UnlockHiddenDecals.IsEnabled)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                
                break;
            }
            case true when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, "DROP VIEW Drivable_Data_Car; CREATE VIEW Drivable_Data_Car AS SELECT Data_Car.* FROM Data_Car; INSERT INTO Data_Car_Buckets(CarId) SELECT Id FROM Data_Car WHERE Id NOT IN (SELECT CarId FROM Data_Car_Buckets); UPDATE Data_Car_Buckets SET CarBucket=0, BucketHero=0 WHERE CarBucket IS NULL                                                                             ");
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, Sql11OriginalString);
                break;
            }
        }
    }

    private void UnlockHiddenPresets_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender)!);
            
        switch (UnlockHiddenPresets.IsEnabled)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, UnlockHiddenPresets_OnToggled, UnlockPresetsString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql11, Sql11EraseString);
                Mw.M.WriteStringMemory(Sql11, UnlockPresetsString);
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, Sql11OriginalString);
                break;
            }
        }
    }

    private void ClearGarageBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _clearGarageString = ClearGarageBox.SelectedIndex switch
        {
            0 => // All
                "DELETE FROM Profile0_Career_Garage WHERE Id > 0;",
            1 => // Dupes
                "DELETE FROM Profile0_Career_Garage WHERE Id NOT IN (select min(Id) from Profile0_Career_Garage group by CarId);",
            2 => // Non favorites
                "DELETE FROM Profile0_Career_Garage WHERE IsFavorite IS NOT 1;",
            3 => // Rare cars
                "DELETE FROM Profile0_Career_Garage WHERE CarId NOT IN (SELECT Id FROM Data_Car WHERE NotAvailableInAutoshow = 0);",
            4 => // Autoshow cars
                "DELETE FROM Profile0_Career_Garage WHERE CarId NOT IN (SELECT Id FROM Data_Car WHERE NotAvailableInAutoshow = 1);",
            5 => // Only untuned
                "DELETE FROM Profile0_Career_Garage WHERE VersionedTuneId IS \"00000000-0000-0000-0000-000000000000\";",
            6 => // Only unpainted
                "DELETE FROM Profile0_Career_Garage WHERE VersionedLiveryId IS \"00000000-0000-0000-0000-000000000000\";",
            _ => _clearGarageString
        };
    }

    private void ClearTag_OnToggled(object sender, RoutedEventArgs e)
    {
        if (!Mw.Attached)
        {
            return;
        }

        ToggleButtons((ToggleSwitch)sender, (bool)sender.GetType().GetProperty("IsOn")?.GetValue(sender)!);
            
        switch (ClearTag.IsOn)
        {
            case true when Mw.Gvp.Name == "Forza Horizon 5":
            {
                ExecSql((ToggleSwitch)sender, ClearTag_OnToggled, ClearTagString);
                break;
            }
            case true:
            {
                Mw.M.WriteStringMemory(Sql11, Sql11EraseString);
                Mw.M.WriteStringMemory(Sql11, ClearTagString);
                break;
            }
            case false when Mw.Gvp.Name == "Forza Horizon 4":
            {
                Mw.M.WriteStringMemory(Sql11, Sql11OriginalString);
                break;
            }
        }
    }
}