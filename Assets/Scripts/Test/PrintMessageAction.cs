using GamepadSupport;
using UnityEngine;

namespace Test
{
    public class PrintMessageAction : IGameAction
    {
        private readonly string _message;

        public PrintMessageAction(string message)
        {
            _message = message;
        }
        
        public void Execute()
        {
            Debug.Log(_message);
        }

        public void Cancel()
        {
            Debug.Log(_message + " canceled");
        }
    }
}