using System.Collections.Generic;
using Test;
using UnityEngine;

namespace GamepadSupport
{
    public class GamepadInputController
    {
        private const float ConnectionCheckTimeout = 2.5f;

        private static readonly List<string> _xboxGamepadNames = new List<string>
        {
            "controller (xbox 360 for windows)", "controller (xbox 360 wireless receiver for windows)", "controller (xbox one for windows)"
        };

        private static readonly List<string> _ps4GamepadNames = new List<string>
        {
            "wireless controller"
        };

        private readonly GamepadButtonWrapper _attackButton;
        private readonly GamepadButtonWrapper _ability1Button;
        private readonly GamepadButtonWrapper _ability2Button;
        private readonly GamepadButtonWrapper _dashButton;
        private readonly GamepadButtonWrapper _jumpButton;
        private readonly GamepadButtonWrapper _interactButton;
        private readonly GamepadButtonWrapper _backButton;
        private readonly GamepadButtonWrapper _pauseButton;

        private bool _isConnected;
        private float _lastCheckTime;

        public static GamepadType GamepadType;

        public bool IsConnected
        {
            get { return _isConnected; }
        }

        public GamepadInputController()
        {
            var inputConfig = Resources.Load<GamepadInputConfig>(GamepadInputConfig.AssetPath);
            if (inputConfig == null)
            {
                Debug.LogWarning("Failed to find GamepadInputConfig asset");
                return;
            }

            _attackButton = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Attack), new PrintMessageAction("Attack"));
            _ability1Button = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Ability1), new PrintMessageAction("Ability1"));
            _ability2Button = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Ability2), new PrintMessageAction("Ability2"));
            _dashButton = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Dash), new PrintMessageAction("Dash"));
            _jumpButton = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Jump), new PrintMessageAction("Jump"));
            _interactButton = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Interact), new PrintMessageAction("Interact"));
            _backButton = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Back), new PrintMessageAction("Back"));
            _pauseButton = new GamepadButtonWrapper(inputConfig.GetEvent(GameActionType.Pause), new PrintMessageAction("Pause"));
        }

        public bool IsMovingLeft()
        {
            if (!_isConnected)
            {
                return false;
            }

            var leftStickXAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxLeftStickXAxis : GamepadInputConfig.Ps4LeftStickXAxis;
            var dPadXAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxDPadXAxis : GamepadInputConfig.Ps4DPadXAxis;

            return Input.GetAxis(leftStickXAxis) < -0.1f || Input.GetAxis(dPadXAxis) < -0.1f;
        }

        public bool IsMovingRight()
        {
            if (!_isConnected)
            {
                return false;
            }

            var leftStickXAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxLeftStickXAxis : GamepadInputConfig.Ps4LeftStickXAxis;
            var dPadXAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxDPadXAxis : GamepadInputConfig.Ps4DPadXAxis;

            return Input.GetAxis(leftStickXAxis) > 0.1f || Input.GetAxis(dPadXAxis) > 0.1f;
        }

        public bool IsMovingDown()
        {
            if (!_isConnected)
            {
                return false;
            }

            var leftStickYAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxLeftStickYAxis : GamepadInputConfig.Ps4LeftStickYAxis;
            var dPadYAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxDPadYAxis : GamepadInputConfig.Ps4DPadYAxis;

            return Input.GetAxis(leftStickYAxis) > 0.1f || Input.GetAxis(dPadYAxis) < -0.1f;
        }

        public bool IsMovingUp()
        {
            if (!_isConnected)
            {
                return false;
            }

            var leftStickYAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxLeftStickYAxis : GamepadInputConfig.Ps4LeftStickYAxis;
            var dPadYAxis = GamepadType == GamepadType.Xbox ? GamepadInputConfig.XboxDPadYAxis : GamepadInputConfig.Ps4DPadYAxis;

            return Input.GetAxis(leftStickYAxis) < -0.1f || Input.GetAxis(dPadYAxis) > 0.1f;
        }

        public void Update()
        {
            CheckConnection();

            if (_isConnected)
            {
                _attackButton.Update();
                _ability1Button.Update();
                _ability2Button.Update();
                _dashButton.Update();
                _jumpButton.Update();
                _interactButton.Update();
                _backButton.Update();
                _pauseButton.Update(true);
            }
        }

        private void CheckConnection()
        {
            if (Time.unscaledTime - _lastCheckTime >= ConnectionCheckTimeout)
            {
                var joystickNames = Input.GetJoystickNames();
                if (joystickNames.Length > 0)
                {
                    var firstConnectedJoystick = joystickNames[0].ToLower();
                    if (!string.IsNullOrEmpty(firstConnectedJoystick))
                    {
                        if (_xboxGamepadNames.Contains(firstConnectedJoystick))
                        {
                            GamepadType = GamepadType.Xbox;
                        }
                        else if (_ps4GamepadNames.Contains(firstConnectedJoystick))
                        {
                            GamepadType = GamepadType.Ps4;
                        }
                        else
                        {
                            GamepadType = GamepadType.Xbox;
                        }

                        if (!_isConnected)
                        {
                            _isConnected = true;
                            Debug.Log(string.Format("Gamepad connected: {0}", GamepadType));
                        }
                    }
                    else
                    {
                        if (_isConnected)
                        {
                            _isConnected = false;
                            Debug.Log(string.Format("Gamepad disconnected: {0}", GamepadType));
                        }
                    }
                }

                _lastCheckTime = Time.unscaledTime;
            }
        }
    }
}