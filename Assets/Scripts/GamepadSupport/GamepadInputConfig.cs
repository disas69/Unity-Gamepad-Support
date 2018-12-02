using System.Collections.Generic;
using UnityEngine;

namespace GamepadSupport
{
    [CreateAssetMenu(fileName = "GamepadInputConfig")]
    public class GamepadInputConfig : ScriptableObject
    {
        public const string AssetPath = "GamepadInputConfig";

        public const string XboxAButton = "Xbox A";
        public const string XboxBButton = "Xbox B";
        public const string XboxXButton = "Xbox X";
        public const string XboxYButton = "Xbox Y";
        public const string XboxLeftBumper = "Xbox LB";
        public const string XboxRightBumper = "Xbox RB";
        public const string XboxLeftTrigger = "Xbox LT";
        public const string XboxRightTrigger = "Xbox RT";
        public const string XboxBackButton = "Xbox Back";
        public const string XboxStartButton = "Xbox Start";
        public const string XboxLeftStickXAxis = "Xbox LS X";
        public const string XboxLeftStickYAxis = "Xbox LS Y";
        public const string XboxRightStickXAxis = "Xbox RS X";
        public const string XboxRightStickYAxis = "Xbox RS Y";
        public const string XboxDPadXAxis = "Xbox D-Pad X";
        public const string XboxDPadYAxis = "Xbox D-Pad Y";

        public const string Ps4XButton = "PS4 X";
        public const string Ps4CircleButton = "PS4 Circle";
        public const string Ps4SquareButton = "PS4 Square";
        public const string Ps4TriangleButton = "PS4 Triangle";
        public const string Ps4LeftBumper = "PS4 L1";
        public const string Ps4RightBumper = "PS4 R1";
        public const string Ps4LeftTrigger = "PS4 L2";
        public const string Ps4RightTrigger = "PS4 R2";
        public const string Ps4ShareButton = "PS4 Share";
        public const string Ps4OptionsButton = "PS4 Options";
        public const string Ps4LeftStickXAxis = "PS4 LS X";
        public const string Ps4LeftStickYAxis = "PS4 LS Y";
        public const string Ps4RightStickXAxis = "PS4 RS X";
        public const string Ps4RightStickYAxis = "PS4 RS Y";
        public const string Ps4DPadXAxis = "PS4 D-Pad X";
        public const string Ps4DPadYAxis = "PS4 D-Pad Y";

        private static readonly Dictionary<GamepadButton, List<GamepadButtonData>> GamepadButtonsMapping = new Dictionary<GamepadButton, List<GamepadButtonData>>(new GamepadButtonTypeEqualityComparer())
        {
            {
                GamepadButton.Button1, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxYButton), new GamepadButtonData(GamepadType.Ps4, Ps4TriangleButton)
                }
            },
            {
                GamepadButton.Button2, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxBButton), new GamepadButtonData(GamepadType.Ps4, Ps4TriangleButton)
                }
            },
            {
                GamepadButton.Button3, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxAButton), new GamepadButtonData(GamepadType.Ps4, Ps4XButton)
                }
            },
            {
                GamepadButton.Button4, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxXButton), new GamepadButtonData(GamepadType.Ps4, Ps4ShareButton)
                }
            },
            {
                GamepadButton.LeftBumper, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxLeftBumper), new GamepadButtonData(GamepadType.Ps4, Ps4LeftBumper)
                }
            },
            {
                GamepadButton.RightBumper, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxRightBumper), new GamepadButtonData(GamepadType.Ps4, Ps4RightBumper)
                }
            },
            {
                GamepadButton.LeftTrigger, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxLeftTrigger), new GamepadButtonData(GamepadType.Ps4, Ps4LeftTrigger)
                }
            },
            {
                GamepadButton.RightTrigger, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxRightTrigger), new GamepadButtonData(GamepadType.Ps4, Ps4RightTrigger)
                }
            },
            {
                GamepadButton.BackButton, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxBackButton), new GamepadButtonData(GamepadType.Ps4, Ps4ShareButton)
                }
            },
            {
                GamepadButton.StartButton, new List<GamepadButtonData>
                {
                    new GamepadButtonData(GamepadType.Xbox, XboxStartButton), new GamepadButtonData(GamepadType.Ps4, Ps4OptionsButton)
                }
            }
        };

        public List<GamepadInputEvent> InputEvents = new List<GamepadInputEvent>
        {
            { new GamepadInputEvent(GamepadButton.Button1, GameActionType.Interact) },
            { new GamepadInputEvent(GamepadButton.Button2, GameActionType.Dash) },
            { new GamepadInputEvent(GamepadButton.Button3, GameActionType.Jump) },
            { new GamepadInputEvent(GamepadButton.Button4, GameActionType.Attack) },
            { new GamepadInputEvent(GamepadButton.LeftBumper, GameActionType.Ability1) },
            { new GamepadInputEvent(GamepadButton.RightBumper, GameActionType.Ability2) },
            { new GamepadInputEvent(GamepadButton.LeftTrigger, GameActionType.None) },
            { new GamepadInputEvent(GamepadButton.RightTrigger, GameActionType.None) },
            { new GamepadInputEvent(GamepadButton.BackButton, GameActionType.Back) },
            { new GamepadInputEvent(GamepadButton.StartButton, GameActionType.Pause) }
        };

        public GamepadInputEvent GetEvent(GameActionType actionType)
        {
            var inputEvent = InputEvents.Find(e => e.Action == actionType);
            if (inputEvent != null)
            {
                return inputEvent;
            }

            Debug.LogError(string.Format("Failed to find GamepadInputEvent of type: {0}", actionType));
            return null;
        }

        public static string GetButtonName(GamepadButton gamepadButton, GamepadType type)
        {
            List<GamepadButtonData> buttonDataList;
            if (GamepadButtonsMapping.TryGetValue(gamepadButton, out buttonDataList))
            {
                var buttonData = buttonDataList.Find(b => b.Type == type);
                if (buttonData != null)
                {
                    return buttonData.Name;
                }
            }

            Debug.LogError(string.Format("Failed to get {0} Gamepad button name: {1}", type, gamepadButton));
            return null;
        }

        private class GamepadButtonTypeEqualityComparer : IEqualityComparer<GamepadButton>
        {
            public bool Equals(GamepadButton x, GamepadButton y)
            {
                return x == y;
            }

            public int GetHashCode(GamepadButton button)
            {
                return ((byte) button).GetHashCode();
            }
        }
    }
}