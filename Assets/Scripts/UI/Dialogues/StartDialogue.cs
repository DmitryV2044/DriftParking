using Scripts.General;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI.Dialogues
{
    public class StartDialogue : Dialogue
    {
        [SerializeField] private Button _startButton;

        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(HandleStart);
        }

        private void HandleStart()
        {
            _eventBus.Dispatch(GameEventType.Started);
            _interactor.HideDialogue();
        }

        private void OnDisable()
        {
            _startButton.onClick.AddListener(HandleStart);
        }
    }
}