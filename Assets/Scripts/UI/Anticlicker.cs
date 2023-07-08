using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI
{
    public class Anticlicker : UIElement, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData) => _interactor.ClosePopup();

    }

}
