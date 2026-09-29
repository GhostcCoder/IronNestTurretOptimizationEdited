using BepInEx.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace IronNestTurretOptimization.Models
{
    internal class SettingsModel
    {
        public static SettingsModel Instance { get; private set; }

        public readonly ConfigEntry<bool> KeyUnlockReloadingElevationSlider;
        public readonly ConfigEntry<bool> KeyAutoReloadLeftGun;
        public readonly ConfigEntry<bool> KeyAutoReloadRightGun;

        public readonly ConfigEntry<int> KeyAutoReloadLeftGunDesiredPowderCharges;
        public readonly ConfigEntry<int> KeyAutoReloadRightGunDesiredPowderCharges;

        public readonly ConfigEntry<float> KeyElevationChangeSpeed;
        public readonly ConfigEntry<float> KeyRotationSpeed;
        public readonly ConfigEntry<float> KeyReloadAnimationSpeed;
        public readonly ConfigEntry<float> KeyCylinderAnimationSpeed;
        public readonly ConfigEntry<float> KeyElevationSliderKeyboardSpeed;
        public readonly ConfigEntry<float> KeyRotationDialKeyboardSpeed;
        public readonly ConfigEntry<float> KeyArtilleryComputerDistanceSliderKeyboardSpeed;
        public readonly ConfigEntry<float> KeyArtilleryComputerRotationDialKeyboardSpeed;

        public readonly ConfigEntry<float> KeyMoveCylinderButtonDelay;
        public readonly ConfigEntry<float> KeyRequisitionButtonDelay;

        [SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
        private SettingsModel(ConfigFile configFile)
        {
            const string mainSettings = "Main Settings";
            const string autoReloadSettings = "Auto Reload Settings";
            const string speedSettings = "Speed Settings";

            KeyUnlockReloadingElevationSlider =
                configFile.Bind<bool>(mainSettings, "Unlock Elevation Slider during Reloading", true);
            KeyAutoReloadLeftGun = configFile.Bind<bool>(mainSettings, "Auto Reload Left Gun", false);
            KeyAutoReloadRightGun = configFile.Bind<bool>(mainSettings, "Auto Reload Right Gun", false);

            KeyAutoReloadLeftGunDesiredPowderCharges =
                configFile.Bind<int>(autoReloadSettings, "Left Gun Desired Powder Charges", 6);
            KeyAutoReloadRightGunDesiredPowderCharges = configFile.Bind<int>(autoReloadSettings,
                "Right Gun Desired Powder Charges", 6);

            KeyElevationChangeSpeed = configFile.Bind<float>(speedSettings, "Elevation Change Speed", 2);
            KeyRotationSpeed = configFile.Bind<float>(speedSettings, "Rotation Speed", 4);
            KeyReloadAnimationSpeed = configFile.Bind<float>(speedSettings, "Reload Animation Speed", 1);
            KeyCylinderAnimationSpeed = configFile.Bind<float>(speedSettings, "Cylinder Animation Speed", 1);
            KeyElevationSliderKeyboardSpeed =
                configFile.Bind<float>(speedSettings, "Elevation Slider Keyboard Speed", 4);
            KeyRotationDialKeyboardSpeed = configFile.Bind<float>(speedSettings, "Rotation Dial Keyboard Speed", 4);
            KeyArtilleryComputerDistanceSliderKeyboardSpeed =
                configFile.Bind<float>(speedSettings, "Artillery Computer Distance Slider Keyboard Speed", 2);
            KeyArtilleryComputerRotationDialKeyboardSpeed =
                configFile.Bind<float>(speedSettings, "Artillery Computer Rotation Dial Keyboard Speed", 4);

            KeyMoveCylinderButtonDelay = configFile.Bind<float>(speedSettings, "Move Cylinder Button Delay", 0.5f);
            KeyRequisitionButtonDelay = configFile.Bind<float>(speedSettings, "Requisition Button Delay", 1.7f);
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public static SettingsModel Create(ConfigFile configFile)
        {
            if (Instance != null)
                return Instance;

            return Instance = new SettingsModel(configFile);
        }
    }
}