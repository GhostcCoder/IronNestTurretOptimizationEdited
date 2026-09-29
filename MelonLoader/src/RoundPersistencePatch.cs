using Il2Cpp;
using MelonLoader;
using System.Linq;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;
using UnityEngine;

namespace IronNestTurretOptimization
{
    // The original mod applies most settings only from Start/Awake.
    // Iron Nest can rebuild/reset turret-related state when changing rounds.
    // This lightweight periodic pass reapplies the same settings and restores
    // the keyboard helper components if the corresponding UI object was rebuilt.
    internal static class RoundPersistencePatch
    {
        private const float ScanInterval = 0.25f;
        private static float _nextScanTime;

        public static void Update()
        {
            if (Time.unscaledTime < _nextScanTime)
                return;

            _nextScanTime = Time.unscaledTime + ScanInterval;

            var settings = SettingsModel.Instance;
            if (settings == null)
                return;

            RunSafe("TurretController", () => ReapplyTurretControllers(settings));
            RunSafe("GunController", () => ReapplyGunControllers(settings));
            RunSafe("ArtilleryReloadController", () => ReapplyReloadControllers(settings));
            RunSafe("CylinderShellSelector", () => ReapplyCylinderSelectors(settings));
            RunSafe("GunElevationSliderBinding", () => ReapplyElevationBindings(settings));
            RunSafe("RequisitionConsoleManager", () => ReapplyRequisitionConsoles(settings));
            RunSafe("AutoReloadManager", () => ReapplyAutoReloadManagers(settings));
            RunSafe("ArtilleryComputer", () => ReapplyArtilleryComputers(settings));
            RunSafe("FireMissionCardPrinter", () => ReapplyFireMissionPrinters(settings));
        }

        private static void RunSafe(string target, System.Action action)
        {
            try
            {
                action();
            }
            catch (System.Exception ex)
            {
                // Round transitions can briefly expose half-rebuilt IL2CPP objects.
                // Log the transient error but keep repairing all other subsystems.
                MelonLogger.Error($"Round persistence {target}: {ex.Message}");
            }
        }

        private static void ReapplyTurretControllers(SettingsModel settings)
        {
            foreach (var turret in UnityEngine.Object.FindObjectsByType<TurretController>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (turret == null)
                    continue;

                turret.maxManualElevationSpeed = settings.KeyElevationChangeSpeed.Value;
                turret.maxManualRotationSpeed = settings.KeyRotationSpeed.Value;
                turret.rotationSpeed = settings.KeyRotationSpeed.Value;
            }

            var turrets = UnityEngine.Object.FindObjectsByType<TurretController>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            var aimingConsole = GameObject.Find("Aiming Console");
            if (aimingConsole == null)
                return;

            var elevationZone = aimingConsole.transform.Find("Zone Camera Elevation");
            if (elevationZone != null)
            {
                var sliderKeyboard = elevationZone.gameObject.GetComponent<SliderKeyboard>();
                if (sliderKeyboard == null)
                    sliderKeyboard = elevationZone.gameObject.AddComponent<SliderKeyboard>();

                sliderKeyboard.SpeedMultiplier = settings.KeyElevationSliderKeyboardSpeed.Value;

                if (turrets.Length > 0 && turrets[0] != null && turrets[0].guns != null)
                {
                    sliderKeyboard.LinearSliderInteractable = turrets[0].guns._items
                        .Select(x => x.GetComponent<GunElevationSliderBinding>().desiredSlider)
                        .ToArray();
                }
            }

            var rotationZone = aimingConsole.transform.Find("Zone Camera Rotation");
            if (rotationZone != null)
            {
                if (turrets.Length == 0 || turrets[0] == null)
                    return;

                var dialKeyboard = rotationZone.gameObject.GetComponent<DialKeyboard>();
                if (dialKeyboard == null)
                    dialKeyboard = rotationZone.gameObject.AddComponent<DialKeyboard>();

                dialKeyboard.SpeedMultiplier = settings.KeyRotationDialKeyboardSpeed.Value;
                dialKeyboard.DialInteractable = new[] { turrets[0].rotationSpeedDial };
            }
        }

        private static void ReapplyGunControllers(SettingsModel settings)
        {
            foreach (var gun in UnityEngine.Object.FindObjectsByType<GunController>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (gun != null)
                    gun.elevationChangeSpeed = settings.KeyElevationChangeSpeed.Value;
            }
        }

        private static void ReapplyReloadControllers(SettingsModel settings)
        {
            foreach (var reload in UnityEngine.Object.FindObjectsByType<ArtilleryReloadController>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (reload == null)
                    continue;

                foreach (var animator in reload.animators)
                {
                    if (animator != null)
                        animator.speed = settings.KeyReloadAnimationSpeed.Value;
                }

                var moveCylinder = reload.transform.Find("Universal Button Move Cylinder");
                if (moveCylinder != null)
                {
                    foreach (var toggler in moveCylinder.GetComponents<AnimatorBoolToggler>())
                    {
                        if (toggler != null)
                            toggler.delay = settings.KeyMoveCylinderButtonDelay.Value;
                    }
                }
            }
        }

        private static void ReapplyCylinderSelectors(SettingsModel settings)
        {
            foreach (var selector in UnityEngine.Object.FindObjectsByType<CylinderShellSelector>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (selector != null && selector.animator != null)
                    selector.animator.speed = settings.KeyCylinderAnimationSpeed.Value;
            }
        }

        private static void ReapplyElevationBindings(SettingsModel settings)
        {
            if (!settings.KeyUnlockReloadingElevationSlider.Value)
                return;

            foreach (var binding in UnityEngine.Object.FindObjectsByType<GunElevationSliderBinding>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (binding == null || binding.desiredInteractable == null || binding.gun == null)
                    continue;

                binding.enabled = false;
                binding.desiredInteractable.enabled = true;

                var parent = binding.desiredInteractable.transform.parent;
                if (parent == null)
                    continue;

                var cover = parent.Find(binding.gun.barrelIndex3D == 0
                    ? ".Loading Cover Left"
                    : ".Loading Cover Right");

                if (cover != null)
                    cover.gameObject.active = false;
            }
        }

        private static void ReapplyRequisitionConsoles(SettingsModel settings)
        {
            foreach (var console in UnityEngine.Object.FindObjectsByType<RequisitionConsoleManager>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (console == null)
                    continue;

                var universalButton = console.transform.Find("Universal Button");
                if (universalButton == null)
                    continue;

                foreach (var toggler in universalButton.GetComponents<AnimatorBoolToggler>())
                {
                    if (toggler != null)
                        toggler.delay = settings.KeyRequisitionButtonDelay.Value;
                }
            }
        }

        private static void ReapplyAutoReloadManagers(SettingsModel settings)
        {
            foreach (var manager in UnityEngine.Object.FindObjectsByType<AutoReloadManager>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (manager == null || manager.gunController == null)
                    continue;

                switch (manager.gunController.barrelIndex3D)
                {
                    case 0 when settings.KeyAutoReloadLeftGun.Value:
                        manager.autoReloadEnabled = true;
                        manager.desiredPowderCharges = settings.KeyAutoReloadLeftGunDesiredPowderCharges.Value;
                        if (manager.autoReloadRoutine == null)
                            manager.StartAutoReload();
                        break;
                    case 1 when settings.KeyAutoReloadRightGun.Value:
                        manager.autoReloadEnabled = true;
                        manager.desiredPowderCharges = settings.KeyAutoReloadRightGunDesiredPowderCharges.Value;
                        if (manager.autoReloadRoutine == null)
                            manager.StartAutoReload();
                        break;
                }
            }
        }

        private static void ReapplyArtilleryComputers(SettingsModel settings)
        {
            foreach (var computer in UnityEngine.Object.FindObjectsByType<ArtilleryComputer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (computer == null || computer.rangeDial == null)
                    continue;

                var zoneCamera = computer.transform.Find("Zone Camera");
                if (zoneCamera == null)
                    continue;

                var dialKeyboard = zoneCamera.gameObject.GetComponent<DialKeyboard>();
                if (dialKeyboard == null)
                    dialKeyboard = zoneCamera.gameObject.AddComponent<DialKeyboard>();

                dialKeyboard.UseVerticalControlMode = true;
                dialKeyboard.StepSize = 0.1f;
                dialKeyboard.SpeedMultiplier = settings.KeyArtilleryComputerDistanceSliderKeyboardSpeed.Value;
                dialKeyboard.DialInteractable = new[] { computer.rangeDial };
            }
        }

        private static void ReapplyFireMissionPrinters(SettingsModel settings)
        {
            foreach (var printer in UnityEngine.Object.FindObjectsByType<FireMissionCardPrinter>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (printer == null || printer.bearingDial == null || printer.transform.parent == null)
                    continue;

                var zoneCamera = printer.transform.parent.Find("Zone Camera");
                if (zoneCamera == null)
                    continue;

                var dialKeyboard = zoneCamera.gameObject.GetComponent<DialKeyboard>();
                if (dialKeyboard == null)
                    dialKeyboard = zoneCamera.gameObject.AddComponent<DialKeyboard>();

                dialKeyboard.InvertDirection = true;
                dialKeyboard.SpeedMultiplier = settings.KeyArtilleryComputerRotationDialKeyboardSpeed.Value;
                dialKeyboard.DialInteractable = new[] { printer.bearingDial };
            }
        }
    }
}
