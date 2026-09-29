using MelonLoader;

namespace IronNestTurretOptimization.Models
{
    internal sealed class SettingsModel
    {
        public static SettingsModel Instance { get; private set; }

        public readonly MelonPreferences_Entry<bool> KeyUnlockReloadingElevationSlider;
        public readonly MelonPreferences_Entry<bool> KeyAutoReloadLeftGun;
        public readonly MelonPreferences_Entry<bool> KeyAutoReloadRightGun;

        public readonly MelonPreferences_Entry<int> KeyAutoReloadLeftGunDesiredPowderCharges;
        public readonly MelonPreferences_Entry<int> KeyAutoReloadRightGunDesiredPowderCharges;

        public readonly MelonPreferences_Entry<float> KeyElevationChangeSpeed;
        public readonly MelonPreferences_Entry<float> KeyRotationSpeed;
        public readonly MelonPreferences_Entry<float> KeyReloadAnimationSpeed;
        public readonly MelonPreferences_Entry<float> KeyCylinderAnimationSpeed;
        public readonly MelonPreferences_Entry<float> KeyElevationSliderKeyboardSpeed;
        public readonly MelonPreferences_Entry<float> KeyRotationDialKeyboardSpeed;
        public readonly MelonPreferences_Entry<float> KeyArtilleryComputerDistanceSliderKeyboardSpeed;
        public readonly MelonPreferences_Entry<float> KeyArtilleryComputerRotationDialKeyboardSpeed;

        public readonly MelonPreferences_Entry<float> KeyMoveCylinderButtonDelay;
        public readonly MelonPreferences_Entry<float> KeyRequisitionButtonDelay;

        private SettingsModel()
        {
            var mainSettings = MelonPreferences.CreateCategory("Main Settings", "Main Settings");
            var autoReloadSettings = MelonPreferences.CreateCategory("Auto Reload Settings", "Auto Reload Settings");
            var speedSettings = MelonPreferences.CreateCategory("Speed Settings", "Speed Settings");

            KeyUnlockReloadingElevationSlider =
                mainSettings.CreateEntry("Unlock Elevation Slider during Reloading", true);
            KeyAutoReloadLeftGun = mainSettings.CreateEntry("Auto Reload Left Gun", false);
            KeyAutoReloadRightGun = mainSettings.CreateEntry("Auto Reload Right Gun", false);

            KeyAutoReloadLeftGunDesiredPowderCharges =
                autoReloadSettings.CreateEntry("Left Gun Desired Powder Charges", 6);
            KeyAutoReloadRightGunDesiredPowderCharges =
                autoReloadSettings.CreateEntry("Right Gun Desired Powder Charges", 6);

            KeyElevationChangeSpeed = speedSettings.CreateEntry("Elevation Change Speed", 2f);
            KeyRotationSpeed = speedSettings.CreateEntry("Rotation Speed", 4f);
            KeyReloadAnimationSpeed = speedSettings.CreateEntry("Reload Animation Speed", 1f);
            KeyCylinderAnimationSpeed = speedSettings.CreateEntry("Cylinder Animation Speed", 1f);
            KeyElevationSliderKeyboardSpeed = speedSettings.CreateEntry("Elevation Slider Keyboard Speed", 4f);
            KeyRotationDialKeyboardSpeed = speedSettings.CreateEntry("Rotation Dial Keyboard Speed", 4f);
            KeyArtilleryComputerDistanceSliderKeyboardSpeed =
                speedSettings.CreateEntry("Artillery Computer Distance Slider Keyboard Speed", 2f);
            KeyArtilleryComputerRotationDialKeyboardSpeed =
                speedSettings.CreateEntry("Artillery Computer Rotation Dial Keyboard Speed", 4f);

            KeyMoveCylinderButtonDelay = speedSettings.CreateEntry("Move Cylinder Button Delay", 0.5f);
            KeyRequisitionButtonDelay = speedSettings.CreateEntry("Requisition Button Delay", 1.7f);
        }

        public static SettingsModel Create()
        {
            if (Instance != null)
                return Instance;

            Instance = new SettingsModel();
            return Instance;
        }
    }
}
