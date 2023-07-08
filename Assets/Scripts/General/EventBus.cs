using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.General
{
    public class EventBus : IDisposable
    {
        private readonly Dictionary<GameEventType, UnityEvent> _events = new();

        public EventBus()
        {
            _events = Enum
                .GetValues(typeof(GameEventType))
                .Cast<GameEventType>()
                .ToDictionary(gameEventType => gameEventType, gameEvent => new UnityEvent());
        }

        public void Subscribe(GameEventType eventType, params UnityAction[] actions)
        {
            if (_events.ContainsKey(eventType))
            {
                foreach (UnityAction action in actions)
                {
                    _events[eventType].AddListener(action);
                }

                return;
            }

            Debug.LogWarning($"Failed to find {eventType} event");
        }

        public void Dispatch(GameEventType eventType)
        {
            if (_events.ContainsKey(eventType))
            {
                _events[eventType]?.Invoke();
            }
        }

        public void UnSubscribe(GameEventType eventType, params UnityAction[] actions)
        {
            if (_events.ContainsKey(eventType))
            {
                foreach (UnityAction action in actions)
                {
                    _events[eventType].RemoveListener(action);
                }

                return;
            }

            Debug.LogWarning($"Failed to find {eventType} event");
        }

        public void Dispose() => _events.Values.ToList().ForEach(gameEvent => gameEvent.RemoveAllListeners());
    }
}