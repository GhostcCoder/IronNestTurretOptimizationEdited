using Il2Cpp;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IronNestTurretOptimization.Components
{
    public class SliderKeyboard : MonoBehaviour
    {
        public SliderKeyboard(IntPtr ptr) : base(ptr) { }

        public LinearSliderInteractable[] LinearSliderInteractable;

        public bool ContinuousMovement = true;

        public float StepSize = 1f;

        public float SpeedMultiplier = 4f;

        private void Update()
        {
            var keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            if (ContinuousMovement)
            {
                var verticalInput = 0f;
                if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                {
                    verticalInput = 1f;
                }
                else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                {
                    verticalInput = -1f;
                }

                if (verticalInput != 0f)
                {
                    foreach (var linearSliderInteractable in LinearSliderInteractable)
                    {
                        linearSliderInteractable.SetSliderValue(linearSliderInteractable.Value +
                                                                verticalInput * StepSize * SpeedMultiplier *
                                                                Time.deltaTime * 10f);
                    }
                }
            }
            else
            {
                if (keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame)
                {
                    foreach (var linearSliderInteractable in LinearSliderInteractable)
                    {
                        linearSliderInteractable.SetSliderValue(linearSliderInteractable.Value +
                                                                StepSize * (linearSliderInteractable.maxOutputValue -
                                                                            linearSliderInteractable.minOutputValue));
                    }
                }

                if (keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame)
                {
                    foreach (var linearSliderInteractable in LinearSliderInteractable)
                    {
                        linearSliderInteractable.SetSliderValue(linearSliderInteractable.Value -
                                                                StepSize * (linearSliderInteractable.maxOutputValue -
                                                                            linearSliderInteractable.minOutputValue));
                    }
                }
            }
        }
    }
}