using HarmonyLib;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(CylinderShellSelector), "Start")]
    public class CylinderShellSelectorPatch
    {
        private static void Postfix(CylinderShellSelector __instance)
        {
            __instance.animator.speed = SettingsModel.Instance.KeyCylinderAnimationSpeed.Value;
        }
    }
}