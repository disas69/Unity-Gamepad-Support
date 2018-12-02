using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamepadSupport
{
    [Serializable]
    public class GamepadInputEvent
    {
        private static readonly List<string> AxisNames = new List<string>
        {
            GamepadInputConfig.XboxLeftTrigger, GamepadInputConfig.XboxRightTrigger, GamepadInputConfig.Ps4LeftTrigger, GamepadInputConfig.Ps4RightTrigger
        };

        public GamepadButton Button;
        public GameActionType Action;

        public GamepadInputEvent(GamepadButton button, GameActionType action)
        {
            Button = button;
            Action = action;
        }

        public bool IsTriggered(GamepadType type)
        {
            var buttonName = GamepadInputConfig.GetButtonName(Button, type);
            if (!string.IsNullOrEmpty(buttonName))
            {
                if (IsAxis(buttonName))
                {
                    return Input.GetAxis(buttonName) >= 1f;
                }

                return Input.GetButton(buttonName);
            }

            Debug.LogWarning(string.Format("Button name for {0}:{1} isn't specified", type, Button));
            return false;
        }

        private static bool IsAxis(string name)
        {
            return AxisNames.Contains(name);
        }
    }
}