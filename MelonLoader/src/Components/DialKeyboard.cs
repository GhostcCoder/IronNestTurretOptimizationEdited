using Il2Cpp;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IronNestTurretOptimization.Components
{
    public class DialKeyboard : MonoBehaviour
    {
        public DialKeyboard(IntPtr ptr) : base(ptr) { }

        public DialInteractable[] DialInteractable;

        public bool ContinuousMovement = true;

        public float StepSize = 1f;

        public float SpeedMultiplier = 4f;

        public bool UseVerticalControlMode;

        public bool InvertDirection;

        private void Update()
        {
            var keyboard = Keyboard.current;

            if (keyboard == null)
                return;

            if (ContinuousMovement)
            {
                var movementInput = 0f;

                if (!UseVerticalControlMode)
                {
                    if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                    {
                        movementInput = -1f;
                    }
                    else if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                    {
                        movementInput = 1f;
                    }
                }
                else
                {
                    if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                    {
                        movementInput = 1f;
                    }
                    else if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                    {
                        movementInput = -1f;
                    }
                }

                if (movementInput != 0f)
                {
                    if (InvertDirection)
                    {
                        movementInput *= -1f;
                    }

                    foreach (var dialInteractable in DialInteractable)
                    {
                        if (!dialInteractable.useDetents)
                        {
                            dialInteractable.currentRotationAngle +=
                                movementInput * StepSize * SpeedMultiplier * Time.deltaTime * 10f;

                            if (dialInteractable.currentRotationAngle > dialInteractable.maxRotationAngle)
                            {
                                dialInteractable.currentRotationAngle = dialInteractable.maxRotationAngle;
                            }
                            else if (dialInteractable.currentRotationAngle < dialInteractable.minRotationAngle)
                            {
                                dialInteractable.currentRotationAngle = dialInteractable.minRotationAngle;
                            }

                            dialInteractable.accumulatedValue =
                                dialInteractable.MapRotationToValue(dialInteractable.currentRotationAngle);
                            dialInteractable.OnValueChanged?.Invoke(dialInteractable.accumulatedValue);
                        }
                        else
                        {
                            dialInteractable.SetDialValue(dialInteractable.accumulatedValue +
                                                          movementInput * StepSize * SpeedMultiplier * Time.deltaTime *
                                                          10f);
                        }
                    }
                }
            }
            else
            {
                if (!UseVerticalControlMode)
                {
                    if (keyboard.leftArrowKey.wasPressedThisFrame || keyboard.aKey.wasPressedThisFrame)
                    {
                        ApplyStep(InvertDirection ? -1f : 1f);
                    }

                    if (keyboard.rightArrowKey.wasPressedThisFrame || keyboard.dKey.wasPressedThisFrame)
                    {
                        ApplyStep(InvertDirection ? 1f : -1f);
                    }
                }
                else
                {
                    if (keyboard.upArrowKey.wasPressedThisFrame || keyboard.wKey.wasPressedThisFrame)
                    {
                        ApplyStep(InvertDirection ? -1f : 1f);
                    }

                    if (keyboard.downArrowKey.wasPressedThisFrame || keyboard.sKey.wasPressedThisFrame)
                    {
                        ApplyStep(InvertDirection ? 1f : -1f);
                    }
                }
            }
        }

        private void ApplyStep(float direction)
        {
            foreach (var dialInteractable in DialInteractable)
            {
                if (!dialInteractable.useDetents)
                {
                    dialInteractable.currentRotationAngle += direction * StepSize *
                                                             (dialInteractable.maxOutputValue -
                                                              dialInteractable.minOutputValue);

                    if (dialInteractable.currentRotationAngle > dialInteractable.maxRotationAngle)
                    {
                        dialInteractable.currentRotationAngle = dialInteractable.maxRotationAngle;
                    }
                    else if (dialInteractable.currentRotationAngle < dialInteractable.minRotationAngle)
                    {
                        dialInteractable.currentRotationAngle = dialInteractable.minRotationAngle;
                    }

                    dialInteractable.accumulatedValue =
                        dialInteractable.MapRotationToValue(dialInteractable.currentRotationAngle);
                    dialInteractable.OnValueChanged?.Invoke(dialInteractable.accumulatedValue);
                }
                else
                {
                    dialInteractable.SetDialValue(dialInteractable.accumulatedValue + direction * StepSize *
                        (dialInteractable.maxOutputValue - dialInteractable.minOutputValue));
                }
            }
        }
    }
}