using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using IronNestTurretOptimization.Components;
using IronNestTurretOptimization.Models;
using UnityEngine;
using System;

namespace IronNestTurretOptimization
{
    [BepInPlugin("com.kmyuhkyuk.IronNestTurretOptimization", "IronNestTurretOptimization", "1.0.1")]
    public class IronNestTurretOptimizationPlugin : BasePlugin
    {
        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<DialKeyboard>();
            ClassInjector.RegisterTypeInIl2Cpp<SliderKeyboard>();
            ClassInjector.RegisterTypeInIl2Cpp<RoundPersistenceRunner>();

            SettingsModel.Create(Config);

            Harmony.CreateAndPatchAll(typeof(IronNestTurretOptimizationPlugin).Assembly);

            var runnerObject = new GameObject("IronNestTurretOptimization_RoundPersistence");
            UnityEngine.Object.DontDestroyOnLoad(runnerObject);
            runnerObject.AddComponent<RoundPersistenceRunner>();

            Log.LogInfo("IronNestTurretOptimization initialized. Round persistence enabled.");
        }
    }

    public class RoundPersistenceRunner : MonoBehaviour
    {
        public RoundPersistenceRunner(IntPtr ptr) : base(ptr) { }

        private void Update()
        {
            RoundPersistencePatch.Update();
        }
    }
}
