using Scripts.General;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Dialogues
{
    public class WinDialogue : Dialogue
    {
        [SerializeField] private Button _restartButton;

        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable() => _restartButton.onClick.AddListener(HandleRestart);

        private void HandleRestart() => _eventBus.Dispatch(GameEventType.Restarted);

        private void OnDisable() => _restartButton.onClick.RemoveAllListeners();
    }
}

