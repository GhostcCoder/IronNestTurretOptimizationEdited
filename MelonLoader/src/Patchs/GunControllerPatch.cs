using Il2Cpp;
using HarmonyLib;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(GunController), "Awake")]
    public class GunControllerPatch
    {
        private static void Postfix(GunController __instance)
        {
            __instance.elevationChangeSpeed = SettingsModel.Instance.KeyElevationChangeSpeed.Value;
        }
    }
}