using Il2Cpp;
using HarmonyLib;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(GunElevationSliderBinding), "Awake")]
    public class GunElevationSliderBindingPatch
    {
        private static void Postfix(GunElevationSliderBinding __instance)
        {
            if (SettingsModel.Instance.KeyUnlockReloadingElevationSlider.Value)
            {
                __instance.enabled = false;
                __instance.desiredInteractable.enabled = true;

                var parent = __instance.desiredInteractable.transform.parent;

                parent.Find(__instance.gun.barrelIndex3D == 0 ? ".Loading Cover Left" : ".Loading Cover Right")
                    .gameObject
                    .active = false;
            }
        }
    }
}