﻿using System.Windows;
using static Forza_Mods_AIO.Resources.Cheats;
using static Forza_Mods_AIO.Resources.Memory;

namespace Forza_Mods_AIO.Cheats.ForzaHorizon5;

public static class CarCheatsOffsets
{
    private const int HookSize = 0x1EB;
    public const int VelEnabled = HookSize;
    public const int VelBoost = HookSize + 1;
    public const int VelLimit = HookSize + 5;
    public const int BrakeHackEnabled = HookSize + 9;
    public const int BrakeHackBoost = HookSize + 10;
    public const int StopAllWheelsEnabled = HookSize + 14;
    public const int JumpHackEnabled = HookSize + 15;
    public const int JumpHackBoost = HookSize + 16;
    public const int WheelspeedEnabled = HookSize + 20;
    public const int WheelspeedMode = HookSize + 21;
    public const int WheelspeedBoost = HookSize + 22;
    public const int WheelspeedLimit = HookSize + 26;
    public const int LocalPlayer = HookSize + 30;
}

public class CarCheats : CheatsUtilities, ICheatsBase, IRevertBase
{
    private UIntPtr _localPlayerHookAddress;
    public UIntPtr LocalPlayerHookDetourAddress;
    private UIntPtr _accelAddress;
    public UIntPtr AccelDetourAddress;
    private UIntPtr _gravityAddress;
    public UIntPtr GravityDetourAddress;
    private UIntPtr _waypointAddress;
    public UIntPtr WaypointDetourAddress;
    private UIntPtr _freezeAiAddress;
    public UIntPtr FreezeAiDetourAddress;
    private UIntPtr _noWaterDragAddress;
    public UIntPtr NoWaterDragDetourAddress;
    private UIntPtr _noClipAddress;
    public UIntPtr NoClipDetourAddress;
    private UIntPtr _racePtr;

    public async Task CheatLocalPlayer()
    {
        _localPlayerHookAddress = 0;
        LocalPlayerHookDetourAddress = 0;

        const string sig = "F3 0F ? ? ? 49 8B ? 49 8B ? 0F 28";
        _localPlayerHookAddress = await SmartAobScan(sig);
        if (_localPlayerHookAddress > 0)
        {
            if (GetClass<Bypass>().CallAddress <= 3)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }

            if (GetClass<Bypass>().CallAddress <= 3) return;

            if (_racePtr <= 0)
            {
                _racePtr = await CheatRacePtr();
            }
            if (_racePtr == 0) return;

            var raceBytes = BitConverter.GetBytes(_racePtr);
            var asm = new byte[]
            {
                0x53, 0x48, 0x83, 0xEC, 0x30, 0xF3, 0x0F, 0x7F, 0x04, 0x24, 0xF3, 0x0F, 0x7F, 0x4C, 0x24, 0x10, 0xF3,
                0x0F, 0x7F, 0x54, 0x24, 0x20, 0x48, 0xB8, raceBytes[0], raceBytes[1], raceBytes[2], raceBytes[3],
                raceBytes[4], raceBytes[5], raceBytes[6], raceBytes[7], 0x48, 0x8B, 0x00, 0x48, 0x8B, 0x40, 0x50, 0x48,
                0x8D, 0x80, 0xD8, 0x03, 0x00, 0x00, 0x81, 0x38, 0x00, 0x00, 0x80, 0x3F, 0x0F, 0x84, 0x8A, 0x01, 0x00,
                0x00, 0xF3, 0x0F, 0x10, 0x4F, 0x20, 0xF3, 0x0F, 0x10, 0x57, 0x28, 0xF3, 0x0F, 0x59, 0xC9, 0xF3, 0x0F,
                0x59, 0xD2, 0xF3, 0x0F, 0x58, 0xCA, 0xF3, 0x0F, 0x51, 0xC1, 0xF3, 0x0F, 0x10, 0xC8, 0x68, 0x29, 0x5C,
                0x0F, 0x40, 0xF3, 0x0F, 0x59, 0x0C, 0x24, 0x48, 0x83, 0xC4, 0x08, 0x80, 0x3D, 0x7E, 0x01, 0x00, 0x00,
                0x01, 0x75, 0x34, 0x0F, 0x2F, 0x0D, 0x7A, 0x01, 0x00, 0x00, 0x77, 0x2B, 0xF3, 0x0F, 0x10, 0x47, 0x20,
                0xF3, 0x0F, 0x10, 0x57, 0x28, 0xF3, 0x0F, 0x59, 0x05, 0x62, 0x01, 0x00, 0x00, 0xF3, 0x0F, 0x59, 0x15,
                0x5A, 0x01, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x47, 0x20, 0xF3, 0x0F, 0x11, 0x57, 0x28, 0xC6, 0x05, 0x48,
                0x01, 0x00, 0x00, 0x00, 0x80, 0x3D, 0x4A, 0x01, 0x00, 0x00, 0x01, 0x75, 0x2B, 0xF3, 0x0F, 0x10, 0x47,
                0x20, 0xF3, 0x0F, 0x10, 0x57, 0x28, 0xF3, 0x0F, 0x59, 0x05, 0x37, 0x01, 0x00, 0x00, 0xF3, 0x0F, 0x59,
                0x15, 0x2F, 0x01, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x47, 0x20, 0xF3, 0x0F, 0x11, 0x57, 0x28, 0xC6, 0x05,
                0x1D, 0x01, 0x00, 0x00, 0x00, 0x80, 0x3D, 0x1C, 0x01, 0x00, 0x00, 0x01, 0x75, 0x19, 0xF3, 0x0F, 0x10,
                0x47, 0x24, 0xF3, 0x0F, 0x58, 0x05, 0x0E, 0x01, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x47, 0x24, 0xC6, 0x05,
                0x01, 0x01, 0x00, 0x00, 0x00, 0x80, 0x3D, 0xF9, 0x00, 0x00, 0x00, 0x01, 0x75, 0x28, 0x48, 0x31, 0xC0,
                0x48, 0x31, 0xDB, 0x48, 0x69, 0xC3, 0xC0, 0x0A, 0x00, 0x00, 0xC7, 0x84, 0x38, 0xC0, 0x26, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x48, 0xFF, 0xC3, 0x48, 0x83, 0xFB, 0x03, 0x76, 0xE5, 0xC6, 0x05, 0xCF, 0x00,
                0x00, 0x00, 0x00, 0x80, 0x3D, 0xCE, 0x00, 0x00, 0x00, 0x01, 0x0F, 0x85, 0x8D, 0x00, 0x00, 0x00, 0x8B,
                0x05, 0xC8, 0x00, 0x00, 0x00, 0x39, 0x87, 0xC0, 0x26, 0x00, 0x00, 0x0F, 0x87, 0x7B, 0x00, 0x00, 0x00,
                0xF3, 0x0F, 0x10, 0x05, 0xB0, 0x00, 0x00, 0x00, 0x68, 0x00, 0x00, 0x20, 0x41, 0xF3, 0x0F, 0x5E, 0x04,
                0x24, 0x48, 0x83, 0xC4, 0x08, 0xF3, 0x0F, 0x10, 0x8F, 0xC0, 0x26, 0x00, 0x00, 0x80, 0x3D, 0x92, 0x00,
                0x00, 0x00, 0x00, 0x75, 0x06, 0xF3, 0x0F, 0x58, 0xC8, 0xEB, 0x28, 0x68, 0x00, 0x00, 0xC8, 0x42, 0xF3,
                0x0F, 0x5E, 0x0C, 0x24, 0x48, 0x83, 0xC4, 0x08, 0xF3, 0x0F, 0x58, 0xC8, 0x68, 0xEC, 0x51, 0x80, 0x3F,
                0xF3, 0x0F, 0x59, 0x0C, 0x24, 0x48, 0x83, 0xC4, 0x08, 0xF3, 0x0F, 0x58, 0x8F, 0xC0, 0x26, 0x00, 0x00,
                0x48, 0x31, 0xC0, 0x48, 0x31, 0xDB, 0x48, 0x69, 0xC3, 0xC0, 0x0A, 0x00, 0x00, 0xF3, 0x0F, 0x11, 0x8C,
                0x38, 0xC0, 0x26, 0x00, 0x00, 0x48, 0xFF, 0xC3, 0x48, 0x83, 0xFB, 0x03, 0x76, 0xE7, 0xC6, 0x05, 0x3B,
                0x00, 0x00, 0x00, 0x00, 0xF3, 0x0F, 0x6F, 0x54, 0x24, 0x20, 0xF3, 0x0F, 0x6F, 0x4C, 0x24, 0x10, 0xF3,
                0x0F, 0x6F, 0x04, 0x24, 0x48, 0x83, 0xC4, 0x30, 0x5B, 0xF3, 0x0F, 0x10, 0x4F, 0x24, 0x48, 0x89, 0x3D,
                0x23, 0x00, 0x00, 0x00
            };
            LocalPlayerHookDetourAddress = GetInstance().CreateDetour(_localPlayerHookAddress, asm, 5);
            return;
        }
        
        ShowError("Local Player Hook", sig);
    }

    private static async Task<UIntPtr> CheatRacePtr()
    {
        const string sig = "48 8B ? ? ? ? ? 48 8B ? ? ? 48 8B ? ? ? ? ? ? 48 85 ? 74 ? 48 8B ? ? 48 85";
        var scanResult = (IntPtr)await SmartAobScan(sig);

        if (scanResult > 0)
        {
            var relativeAddress = scanResult + 3;
            var relative = GetInstance().ReadMemory<int>((nuint)relativeAddress);
            return (nuint)(scanResult + relative + 0x7);
        }

        ShowError("Race Ptr", sig);
        return 0;
    }
    
    public async Task CheatAccel()
    {
        _accelAddress = 0;
        AccelDetourAddress = 0;

        const string sig = "F3 0F ? ? ? 41 0F ? ? 0F C6 DB ? 41 0F";
        _accelAddress = await SmartAobScan(sig);
        if (_accelAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }

            if (LocalPlayerHookDetourAddress == 0)
            {
                return;
            }
            
            var localPlayerAddr = BitConverter.GetBytes(LocalPlayerHookDetourAddress + CarCheatsOffsets.LocalPlayer);
            var raceBytes = BitConverter.GetBytes(_racePtr);
            var asm = new byte[]
            {
                0xF3, 0x0F, 0x10, 0x5D, 0x0C, 0x50, 0x48, 0xB8, raceBytes[0], raceBytes[1], raceBytes[2], raceBytes[3],
                raceBytes[4], raceBytes[5], raceBytes[6], raceBytes[7], 0x48, 0x8B, 0x00, 0x48, 0x8B, 0x40, 0x50, 0x48,
                0x8D, 0x80, 0xD8, 0x03, 0x00, 0x00, 0x81, 0x38, 0x00, 0x00, 0x80, 0x3F, 0x0F, 0x84, 0x28, 0x00, 0x00,
                0x00, 0x48, 0xB8, localPlayerAddr[0], localPlayerAddr[1], localPlayerAddr[2], localPlayerAddr[3],
                localPlayerAddr[4], localPlayerAddr[5], localPlayerAddr[6], localPlayerAddr[7], 0x48, 0x39, 0x28, 0x75,
                0x19, 0xF3, 0x0F, 0x11, 0x1D, 0x1C, 0x00, 0x00, 0x00, 0x80, 0x3D, 0x10, 0x00, 0x00, 0x00, 0x01, 0x75,
                0x08, 0xF3, 0x0F, 0x10, 0x1D, 0x07, 0x00, 0x00, 0x00, 0x58
            };
            AccelDetourAddress = GetInstance().CreateDetour(_accelAddress, asm, 5);
            return;
        }
        
        ShowError("Accel", sig);
    }
    
    public async Task CheatGravity()
    {
        _gravityAddress = 0;
        GravityDetourAddress = 0;

        const string sig = "F3 0F ? ? ? F3 0F ? ? ? ? ? ? F3 0F ? ? ? ? ? ? 45 84 ? 74";
        _gravityAddress = await SmartAobScan(sig);
        if (_gravityAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }

            if (LocalPlayerHookDetourAddress == 0)
            {
                return;
            }
            
            var localPlayerAddr = BitConverter.GetBytes(LocalPlayerHookDetourAddress + CarCheatsOffsets.LocalPlayer);
            var raceBytes = BitConverter.GetBytes(_racePtr);
            var asm = new byte[]
            {
                0x50, 0x51, 0x48, 0xB8, raceBytes[0], raceBytes[1], raceBytes[2], raceBytes[3], raceBytes[4],
                raceBytes[5], raceBytes[6], raceBytes[7], 0x48, 0x8B, 0x00, 0x48, 0x8B, 0x40, 0x50, 0x48, 0x8D, 0x80,
                0xD8, 0x03, 0x00, 0x00, 0x81, 0x38, 0x00, 0x00, 0x80, 0x3F, 0x74, 0x2B, 0x48, 0xB8, localPlayerAddr[0],
                localPlayerAddr[1], localPlayerAddr[2], localPlayerAddr[3], localPlayerAddr[4], localPlayerAddr[5],
                localPlayerAddr[6], localPlayerAddr[7], 0x48, 0x39, 0x18, 0x75, 0x1C, 0x8B, 0x4B, 0x08, 0x89, 0x0D,
                0x24, 0x00, 0x00, 0x00, 0x80, 0x3D, 0x18, 0x00, 0x00, 0x00, 0x01, 0x75, 0x0A, 0xF3, 0x0F, 0x59, 0x0D,
                0x0F, 0x00, 0x00, 0x00, 0xEB, 0x05, 0xF3, 0x0F, 0x59, 0x4B, 0x08, 0x59, 0x58
            };
            GravityDetourAddress = GetInstance().CreateDetour(_gravityAddress, asm, 5);
            return;
        }
        
        ShowError("Gravity", sig);
    }

    public async Task CheatWaypoint()
    {
        _waypointAddress = 0;
        WaypointDetourAddress = 0;

        const string sig = "0F 10 ? ? ? ? ? 0F 28 ? 0F C2 ? 00 0F 50";
        _waypointAddress = await SmartAobScan(sig);
        if (_waypointAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }

            if (LocalPlayerHookDetourAddress == 0)
            {
                return;
            }

            var localPlayerAddr = BitConverter.GetBytes(LocalPlayerHookDetourAddress + CarCheatsOffsets.LocalPlayer);
            var asm = new byte[]
            {
                0x0F, 0x10, 0x97, 0x30, 0x02, 0x00, 0x00, 0x80, 0x3D, 0x24, 0x00, 0x00, 0x00, 0x01, 0x75, 0x1D, 0x50,
                0x48, 0xB8, localPlayerAddr[0], localPlayerAddr[1], localPlayerAddr[2], localPlayerAddr[3],
                localPlayerAddr[4], localPlayerAddr[5], localPlayerAddr[6], localPlayerAddr[7], 0x48, 0x8B, 0x00, 0x48,
                0x85, 0xC0, 0x74, 0x09, 0x0F, 0x11, 0x50, 0x50, 0x44, 0x0F, 0x11, 0x78, 0x20, 0x58
            };
            
            WaypointDetourAddress = GetInstance().CreateDetour(_waypointAddress, asm, 7);
            return;
        }
        
        ShowError("Waypoint", sig);
    }

    public async Task CheatFreezeAi()
    {
        _freezeAiAddress = 0;
        FreezeAiDetourAddress = 0;

        const string sig = "F3 0F ? ? ? ? ? ? F3 0F ? ? F3 0F ? ? 0F 57 ? F3 0F ? ? ? ? ? ? F3 0F ? ? C3";
        _freezeAiAddress = await SmartAobScan(sig);

        if (_freezeAiAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }
            
            if (LocalPlayerHookDetourAddress == 0) return;

            var localPlayer = BitConverter.GetBytes(LocalPlayerHookDetourAddress + CarCheatsOffsets.LocalPlayer);
            var asm = new byte[]
            {
                0x80, 0x3D, 0x48, 0x00, 0x00, 0x00, 0x01, 0x75, 0x39, 0x56, 0x48, 0xBE, localPlayer[0], localPlayer[1],
                localPlayer[2], localPlayer[3], localPlayer[4], localPlayer[5], localPlayer[6], localPlayer[7], 0x48,
                0x8B, 0x36, 0x48, 0x8D, 0xB6, 0x70, 0x05, 0x00, 0x00, 0x48, 0x39, 0xCE, 0x74, 0x1E, 0xC7, 0x81, 0xB0,
                0xFA, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0xC7, 0x81, 0xB4, 0xFA, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00,
                0xC7, 0x81, 0xB8, 0xFA, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x5E, 0xF3, 0x0F, 0x58, 0x81, 0x54, 0x01,
                0x00, 0x00
            };

            FreezeAiDetourAddress = GetInstance().CreateDetour(_freezeAiAddress, asm, 8);
            return;
        }
        
        ShowError("Freeze Ai", sig);
    }

    public async Task CheatNoWaterDrag()
    {
        _noWaterDragAddress = 0;
        NoWaterDragDetourAddress = 0;

        const string sig = "48 8B ? F3 0F ? ? ? 53 55";
        _noWaterDragAddress = await SmartAobScan(sig);

        if (_noWaterDragAddress > 0)
        {
            if (GetClass<Bypass>().CallAddress <= 3)
            {
                await GetClass<Bypass>().DisableCrcChecks();
            }

            if (GetClass<Bypass>().CallAddress <= 3) return;

            var asm = new byte[]
            {
                0x80, 0x3D, 0x10, 0x00, 0x00, 0x00, 0x01, 0x75, 0x01, 0xC3, 0x48, 0x8B, 0xC4, 0xF3, 0x0F, 0x11, 0x48,
                0x10
            };

            NoWaterDragDetourAddress = GetInstance().CreateDetour(_noWaterDragAddress, asm, 8);
            return;
        }    
        
        ShowError("No water drag", sig);
    }
    
    public async Task CheatNoClip()
    {
        _noClipAddress = 0;
        NoClipDetourAddress = 0;

        const string sig = "48 8B ? 4C 89 ? ? 56 41 ? 41";
        _noClipAddress = await SmartAobScan(sig);

        if (_noClipAddress > 0)
        {
            if (LocalPlayerHookDetourAddress == 0)
            {
                await CheatLocalPlayer();
            }
            
            if (LocalPlayerHookDetourAddress == 0) return;
            
            var localPlayer = BitConverter.GetBytes(LocalPlayerHookDetourAddress + CarCheatsOffsets.LocalPlayer);
            var asm = new byte[]
            {
                0x80, 0x3D, 0x2A, 0x00, 0x00, 0x00, 0x01, 0x75, 0x1C, 0x48, 0xB8, localPlayer[0], localPlayer[1],
                localPlayer[2], localPlayer[3], localPlayer[4], localPlayer[5], localPlayer[6], localPlayer[7], 0x48,
                0x8B, 0x00, 0x48, 0x85, 0xC0, 0x74, 0x0A, 0x83, 0xB8, 0x88, 0x1A, 0x00, 0x00, 0x00, 0x75, 0x01, 0xC3,
                0x48, 0x8B, 0xC4, 0x4C, 0x89, 0x48, 0x20
            };

            NoClipDetourAddress = GetInstance().CreateDetour(_noClipAddress, asm, 7);
            return;
        }
        
        ShowError("No clip", sig);
    }
    
    public void Cleanup()
    {
        var mem = GetInstance();
        
        if (AccelDetourAddress > 0)
        {
            mem.WriteArrayMemory(_accelAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x5D, 0x0C });
            Free(AccelDetourAddress);
        }

        if (GravityDetourAddress > 0)
        {
            mem.WriteArrayMemory(_gravityAddress, new byte[] { 0xF3, 0x0F, 0x59, 0x4B, 0x08 });
            Free(GravityDetourAddress);
        }

        if (_waypointAddress > 0)
        {
            mem.WriteArrayMemory(_waypointAddress, new byte[] { 0x0F, 0x10, 0x97, 0x30, 0x02, 0x00, 0x00 });
            Free(WaypointDetourAddress);
        }

        if (_freezeAiAddress > 0)
        {
            mem.WriteArrayMemory(_freezeAiAddress, new byte[] { 0xF3, 0x0F, 0x58, 0x81, 0x54, 0x01, 0x00, 0x00 });
            Free(FreezeAiDetourAddress);            
        }

        if (_noWaterDragAddress > 0)
        {
            mem.WriteArrayMemory(_noWaterDragAddress, new byte[] { 0x48, 0x8B, 0xC4, 0xF3, 0x0F, 0x11, 0x48, 0x10 });
            Free(NoWaterDragDetourAddress);
        }

        if (_noClipAddress > 0)
        {
            mem.WriteArrayMemory(_noClipAddress, new byte[] { 0x48, 0x8B, 0xC4, 0x4C, 0x89, 0x48, 0x20 });
            Free(NoClipDetourAddress);
        }
        
        if (LocalPlayerHookDetourAddress <= 0) return;
        mem.WriteArrayMemory(_localPlayerHookAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x4F, 0x24 });
        Free(LocalPlayerHookDetourAddress);
    }

    public void Reset()
    {
        var fields = typeof(CarCheats).GetFields().Where(f => f.FieldType == typeof(UIntPtr));
        foreach (var field in fields)
        {
            field.SetValue(this, UIntPtr.Zero);
        }
    }

    public void Revert()
    {        
        var mem = GetInstance();
        
        if (AccelDetourAddress > 0)
        {
            mem.WriteArrayMemory(_accelAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x5D, 0x0C });
        }

        if (GravityDetourAddress > 0)
        {
            mem.WriteArrayMemory(_gravityAddress, new byte[] { 0xF3, 0x0F, 0x59, 0x4B, 0x08 });
        }

        if (_waypointAddress > 0)
        {
            mem.WriteArrayMemory(_waypointAddress, new byte[] { 0x0F, 0x10, 0x97, 0x30, 0x02, 0x00, 0x00 });
        }

        if (_freezeAiAddress > 0)
        {
            mem.WriteArrayMemory(_freezeAiAddress, new byte[] { 0xF3, 0x0F, 0x58, 0x81, 0x54, 0x01, 0x00, 0x00 });
        }

        if (_noWaterDragAddress > 0)
        {
            mem.WriteArrayMemory(_noWaterDragAddress, new byte[] { 0x48, 0x8B, 0xC4, 0xF3, 0x0F, 0x11, 0x48, 0x10 });
        }

        if (_noClipAddress > 0)
        {
            mem.WriteArrayMemory(_noClipAddress, new byte[] { 0x48, 0x8B, 0xC4, 0x4C, 0x89, 0x48, 0x20 });
        }
        
        if (LocalPlayerHookDetourAddress <= 0) return;
        mem.WriteArrayMemory(_localPlayerHookAddress, new byte[] { 0xF3, 0x0F, 0x10, 0x4F, 0x24 });
    }

    public void Continue()
    {
        var mem = GetInstance();
        
        if (AccelDetourAddress > 0)
        {
            mem.WriteArrayMemory(_accelAddress, CalculateDetour(_accelAddress, AccelDetourAddress, 5));
        }

        if (GravityDetourAddress > 0)
        {
            mem.WriteArrayMemory(_gravityAddress, CalculateDetour(_gravityAddress, GravityDetourAddress, 5));
        }

        if (_waypointAddress > 0)
        {
            mem.WriteArrayMemory(_waypointAddress, CalculateDetour(_waypointAddress, WaypointDetourAddress, 7));
        }

        if (_freezeAiAddress > 0)
        {
            mem.WriteArrayMemory(_freezeAiAddress, CalculateDetour(_freezeAiAddress, FreezeAiDetourAddress, 8));
        }

        if (_noWaterDragAddress > 0)
        {
            mem.WriteArrayMemory(_noWaterDragAddress, CalculateDetour(_noWaterDragAddress, NoWaterDragDetourAddress, 7));
        }

        if (_noClipAddress > 0)
        {
            mem.WriteArrayMemory(_noClipAddress, CalculateDetour(_noClipAddress, NoClipDetourAddress, 7));
        }
        
        if (LocalPlayerHookDetourAddress <= 0) return;
        mem.WriteArrayMemory(_localPlayerHookAddress, CalculateDetour(_localPlayerHookAddress, LocalPlayerHookDetourAddress, 5));
    }
}