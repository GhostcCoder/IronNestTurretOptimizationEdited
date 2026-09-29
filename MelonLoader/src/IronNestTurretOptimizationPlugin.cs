using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;

[assembly: MelonInfo(typeof(IronNestTurretOptimization.IronNestTurretOptimizationPlugin), "IronNestTurretOptimization", "1.0.1", "kmyuhkyuk / MelonLoader Port")]

namespace IronNestTurretOptimization
{
    public class IronNestTurretOptimizationPlugin : MelonMod
    {
        public override void OnInitializeMelon()
        {
            // These are custom IL2CPP MonoBehaviours added by the original BepInEx mod.
            ClassInjector.RegisterTypeInIl2Cpp<DialKeyboard>();
            ClassInjector.RegisterTypeInIl2Cpp<SliderKeyboard>();

            SettingsModel.Create();

            MelonLogger.Msg("IronNestTurretOptimization initialized. Round persistence enabled.");
        }

        public override void OnUpdate()
        {
            RoundPersistencePatch.Update();
        }
    }
}
