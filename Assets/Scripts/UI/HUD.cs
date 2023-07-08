using Scripts.General;
using Scripts.UI.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    public class HUD : UIElement
    {
        [SerializeField] private Button _pauseButton;

        private EventBus _eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void OnEnable()
        {
            _pauseButton.onClick.AddListener(HandlePause);
        }

        private void HandlePause()
        {
            _interactor.OpenPopup<PausePopup>();
            _eventBus.Dispatch(GameEventType.Paused);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveAllListeners();
        }

    }

}
