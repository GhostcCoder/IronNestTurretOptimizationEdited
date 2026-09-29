using HarmonyLib;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;
using UnityEngine;

namespace IronNestTurretOptimization.Patchs
{
    [HarmonyPatch(typeof(TurretController), "Start")]
    public class TurretControllerPatch
    {
        private static void Postfix(TurretController __instance)
        {
            var settingsModel = SettingsModel.Instance;

            __instance.maxManualElevationSpeed = settingsModel.KeyElevationChangeSpeed.Value;
            __instance.maxManualRotationSpeed = settingsModel.KeyRotationSpeed.Value;
            __instance.rotationSpeed = settingsModel.KeyRotationSpeed.Value;

            var aimingConsoleTransform = GameObject.Find("Aiming Console").transform;

            var elevationSliderKeyboard = aimingConsoleTransform.Find("Zone Camera Elevation").gameObject
                .AddComponent<SliderKeyboard>();

            elevationSliderKeyboard.SpeedMultiplier = settingsModel.KeyElevationSliderKeyboardSpeed.Value;
            elevationSliderKeyboard.LinearSliderInteractable = __instance.guns._items.Select(x =>
                x.GetComponent<GunElevationSliderBinding>().desiredSlider).ToArray();

            var rotationSliderKeyboard = aimingConsoleTransform.Find("Zone Camera Rotation").gameObject.AddComponent<DialKeyboard>();

            rotationSliderKeyboard.SpeedMultiplier = settingsModel.KeyRotationDialKeyboardSpeed.Value;
            rotationSliderKeyboard.DialInteractable = new[] { __instance.rotationSpeedDial };
        }
    }
}