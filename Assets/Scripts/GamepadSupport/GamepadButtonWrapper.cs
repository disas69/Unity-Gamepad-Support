using UnityEngine;

namespace GamepadSupport
{
    public class GamepadButtonWrapper
    {
        private GamepadInputEvent _inputEvent;
        private IGameAction _action;
        private bool _gamepadInputTriggered;

        public GamepadButtonWrapper(GamepadInputEvent inputEvent, IGameAction action)
        {
            _inputEvent = inputEvent;
            _action = action;
        }

        public void Update(bool forceUpdate = false)
        {
            if (_inputEvent == null || Time.timeScale <= 0f && !forceUpdate)
            {
                return;
            }

            if (_inputEvent.IsTriggered(GamepadInputController.GamepadType))
            {
                if (!_gamepadInputTriggered)
                {
                    _gamepadInputTriggered = true;
                    _action.Execute();
                }
            }
            else
            {
                if (_gamepadInputTriggered)
                {
                    _gamepadInputTriggered = false;
                    _action.Cancel();
                }
            }
        }
    }
}