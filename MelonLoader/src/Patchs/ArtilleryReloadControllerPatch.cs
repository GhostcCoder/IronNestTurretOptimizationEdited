using Il2Cpp;
using HarmonyLib;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(ArtilleryReloadController), "Start")]
    public class ArtilleryReloadControllerPatch
    {
        private static void Postfix(ArtilleryReloadController __instance)
        {
            var settingsModel = SettingsModel.Instance;

            foreach (var animator in __instance.animators)
            {
                animator.speed = settingsModel.KeyReloadAnimationSpeed.Value;
            }

            foreach (var animatorBoolToggler in __instance.transform.Find("Universal Button Move Cylinder")
                         .GetComponents<AnimatorBoolToggler>())
            {
                animatorBoolToggler.delay = settingsModel.KeyMoveCylinderButtonDelay.Value;
            }
        }
    }
}