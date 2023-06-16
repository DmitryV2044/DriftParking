using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Scripts.InputHandling
{
    public class RotationControl : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {

        [SerializeField, InputControl(layout = "Button")] private string _controlPath;

        protected override string controlPathInternal { get => _controlPath; set => _controlPath = value; }

        public void OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0f);
        }


    }

}

