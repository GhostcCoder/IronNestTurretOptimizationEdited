using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization
{
    [BepInPlugin("com.kmyuhkyuk.IronNestTurretOptimization", "IronNestTurretOptimization", "1.0.0")]
    public class IronNestTurretOptimizationPlugin : BasePlugin
    {
        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<DialKeyboard>();
            ClassInjector.RegisterTypeInIl2Cpp<SliderKeyboard>();

            SettingsModel.Create(Config);

            Harmony.CreateAndPatchAll(typeof(IronNestTurretOptimizationPlugin).Assembly);
        }
    }
}