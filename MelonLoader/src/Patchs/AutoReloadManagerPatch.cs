using Il2Cpp;
using HarmonyLib;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(AutoReloadManager), "OnEnable")]
    public class AutoReloadManagerOnEnablePatch
    {
        private static void Postfix(AutoReloadManager __instance)
        {
            var settingsModel = SettingsModel.Instance;

            switch (__instance.gunController.barrelIndex3D)
            {
                case 0 when settingsModel.KeyAutoReloadLeftGun.Value:
                    __instance.autoReloadEnabled = true;
                    __instance.desiredPowderCharges = settingsModel.KeyAutoReloadLeftGunDesiredPowderCharges.Value;
                    break;
                case 1 when settingsModel.KeyAutoReloadRightGun.Value:
                    __instance.autoReloadEnabled = true;
                    __instance.desiredPowderCharges = settingsModel.KeyAutoReloadRightGunDesiredPowderCharges.Value;
                    break;
            }
        }
    }

    [HarmonyPatch(typeof(AutoReloadManager), "Update")]
    public class AutoReloadManagerUpdatePatch
    {
        private static void Postfix(AutoReloadManager __instance)
        {
            if (__instance.autoReloadEnabled && __instance.autoReloadRoutine == null)
            {
                __instance.StartAutoReload();
            }
        }
    }
}