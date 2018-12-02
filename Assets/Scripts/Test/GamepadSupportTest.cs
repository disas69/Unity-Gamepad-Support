using GamepadSupport;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class GamepadSupportTest : MonoBehaviour
    {
        private GamepadInputController _gamepadInputController;

        public Text Message;

        private void Start()
        {
            _gamepadInputController = new GamepadInputController();
        }

        private void Update()
        {
            _gamepadInputController.Update();
            Message.gameObject.SetActive(!_gamepadInputController.IsConnected);

            if (_gamepadInputController.IsMovingLeft())
            {
                Debug.Log("Moving left");
            }
            else if (_gamepadInputController.IsMovingRight())
            {
                Debug.Log("Moving right");
            }

            if (_gamepadInputController.IsMovingUp())
            {
                Debug.Log("Moving up");
            }
            else if (_gamepadInputController.IsMovingDown())
            {
                Debug.Log("Moving down");
            }
        }
    }
}