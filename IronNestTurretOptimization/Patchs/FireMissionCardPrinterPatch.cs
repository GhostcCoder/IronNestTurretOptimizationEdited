using HarmonyLib;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(FireMissionCardPrinter), "Awake")]
    public class FireMissionCardPrinterPatch
    {
        private static void Postfix(FireMissionCardPrinter __instance)
        {
            var dialKeyboard = __instance.transform.parent.Find("Zone Camera").gameObject.AddComponent<DialKeyboard>();

            dialKeyboard.InvertDirection = true;
            dialKeyboard.SpeedMultiplier = SettingsModel.Instance.KeyArtilleryComputerRotationDialKeyboardSpeed.Value;
            dialKeyboard.DialInteractable = new[] { __instance.bearingDial };
        }
    }
}