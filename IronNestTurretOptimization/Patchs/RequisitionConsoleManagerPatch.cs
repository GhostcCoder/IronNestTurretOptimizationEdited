using HarmonyLib;
using IronNestTurretOptimization.Models;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(RequisitionConsoleManager), "Start")]
    public class RequisitionConsoleManagerPatch
    {
        private static void Postfix(RequisitionConsoleManager __instance)
        {
            foreach (var animatorBoolToggler in __instance.transform.Find("Universal Button")
                         .GetComponents<AnimatorBoolToggler>())
            {
                animatorBoolToggler.delay = SettingsModel.Instance.KeyRequisitionButtonDelay.Value;
            }
        }
    }
}