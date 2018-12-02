using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class DebugDisplay : MonoBehaviour
    {
        private readonly List<MessageData> _messages = new List<MessageData>();
        private static DebugDisplay _instance;

        [SerializeField] private Text _text;
        [SerializeField] private bool _debugLogEnabled;
        [SerializeField] private float _messageShowDuration = 5f;

        private struct MessageData
        {
            public string Message;
            public float Duration;
            public float StartingTime;
        }

        private void Awake()
        {
            _instance = this;

            if (_debugLogEnabled)
            {
                Application.logMessageReceived += OnLogMessageReceived;
            }
        }

        public static void Show(string message, float duration = 1)
        {
            _instance._messages.Add(new MessageData
            {
                Message = message,
                Duration = duration,
                StartingTime = Time.time,
            });
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            Show(condition, _messageShowDuration);
        }

        private void Update()
        {
            var stringBuilder = new StringBuilder();

            foreach (var message in _messages)
            {
                stringBuilder.AppendLine(message.Message);
                stringBuilder.AppendLine();
            }

            if (_text != null)
            {
                _text.text = stringBuilder.ToString();
            }

            CleanupMessages();
        }

        private void CleanupMessages()
        {
            for (int i = _messages.Count - 1; i >= 0; i--)
            {
                if (Time.time - _messages[i].StartingTime > _messages[i].Duration)
                {
                    _messages.RemoveAt(i);
                }
            }
        }
    }
}