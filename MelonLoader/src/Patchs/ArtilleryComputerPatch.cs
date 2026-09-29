using Il2Cpp;
using HarmonyLib;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(ArtilleryComputer), "Start")]
    public class ArtilleryComputerPatch
    {
        private static void Postfix(ArtilleryComputer __instance)
        {
            var dialKeyboard = __instance.transform.Find("Zone Camera").gameObject.AddComponent<DialKeyboard>();

            dialKeyboard.UseVerticalControlMode = true;
            dialKeyboard.StepSize = 0.1f;
            dialKeyboard.SpeedMultiplier = SettingsModel.Instance.KeyArtilleryComputerDistanceSliderKeyboardSpeed.Value;
            dialKeyboard.DialInteractable = new[] { __instance.rangeDial };
        }
    }
}