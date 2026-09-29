# Iron Nest Turret Optimization - Community Fork

A community-maintained fork of **Iron Nest: Heavy Turret Simulator - Turret Optimization** by [kmyuhkyuk](https://github.com/kmyuhkyuk/IronNestTurretOptimization).

## What this fork changes

### Round Persistence Fix

The original mod can lose some of its applied turret settings when the game transitions to a new round. This fork adds a round-persistence mechanism that reapplies the relevant settings after the game recreates or resets turret-related objects.

The fix was tested in-game across a round transition.

### MelonLoader Port

A separate MelonLoader implementation is included for users who run the game with MelonLoader instead of BepInEx.

## Versions

| Folder | Loader | Description |
|---|---|---|
| `BepInEx/` | BepInEx 6 | Original BepInEx implementation with the round-persistence fix |
| `MelonLoader/` | MelonLoader 0.7.x | MelonLoader port with the round-persistence fix |

Use **only the version matching your installed mod loader**.

## Installation / Building

Both projects include their own setup, build, and install scripts.

1. Open the folder for your mod loader.
2. Run `SetupGameAssemblies.bat`.
3. When prompted, enter the full path to your Iron Nest game directory.
4. Run `Build.bat`.
5. Run `Install.bat`, or copy the generated DLL from the `Build` folder to the appropriate mod directory.

The setup scripts do not require a hard-coded game installation path; the user enters the path manually.

## Source and attribution

This repository is a community fork of the original project:

https://github.com/kmyuhkyuk/IronNestTurretOptimization

Original author: **kmyuhkyuk**

The original project and this fork are distributed under the **GNU General Public License v3.0 (GPL-3.0)**. See `LICENSE.txt` for the license text.

The BepInEx implementation is based on the original project with the round-persistence changes added. The MelonLoader implementation is a separate port of the original functionality, with the same round-persistence changes.

## Notes

- This is a community modification and is not an official update from the original author.
- Game/loader versions can change. Compatibility may depend on the current Iron Nest game build and generated IL2CPP assemblies.
- Generated reference assemblies and build output are intentionally not included in the repository. Run the setup script against your own game installation to generate the required references.
