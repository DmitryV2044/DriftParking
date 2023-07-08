using Scripts.General;
using UnityEngine.EventSystems;
using Zenject;

namespace Scripts.UI
{
    public class Anticlicker : UIElement, IPointerDownHandler
    {
        private EventBus _eventBus;
        [Inject]
        private void Construct(EventBus eventBus) => _eventBus = eventBus;
        public void OnPointerDown(PointerEventData eventData)
        {
            _eventBus.Dispatch(GameEventType.Paused);
            _interactor.ClosePopup();
        }

    }

}
