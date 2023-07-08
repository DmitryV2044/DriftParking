using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Scripts.UI
{
    public class UIInteractor : MonoBehaviour
    {
        [SerializeField] private Controls _controls;
        [SerializeField] private List<Dialogue> _dialogues;
        [SerializeField] private List<Popup> _popups;
        [SerializeField] private Anticlicker _anticlicker;
        [SerializeField] private HUD _hud;

        private Dialogue _currentDialogue;
        private Popup _currentPopup;

        public void ShowDialogue<D>() where D : Dialogue
        {
            HideDialogue();

            Dialogue dialogue = _dialogues.Find(d => d is D);
            _currentDialogue = dialogue;
            _currentDialogue?.Show();

            HideControls();
        }

        public void OpenPopup<P>() where P : Popup
        {
            ClosePopup();

            Popup popup = _popups.Find(p => p is P);
            _currentPopup = popup;
            _currentPopup.Open();
            _anticlicker.gameObject.SetActive(true);
        }

        public void ShowControls() => _controls.gameObject.SetActive(true);
        public void ShowHud() => _hud.gameObject.SetActive(true);


        public void HideDialogue()
        {
            _currentDialogue?.Hide();
            _currentDialogue = null;

            ShowControls();
        }

        public void ClosePopup()
        {
            _currentPopup?.Close();
            _currentPopup = null;
            _anticlicker.gameObject.SetActive(false);
        }

        public void HideControls() => _controls.gameObject.SetActive(false);
        public void HideHud() => _hud.gameObject.SetActive(true);


        [Button]
        private void FindElements()
        {
            _hud = GetComponentInChildren<HUD>(true);
            _anticlicker = GetComponentInChildren<Anticlicker>(true);
            _controls = GetComponentInChildren<Controls>(true);
            _dialogues = new(GetComponentsInChildren<Dialogue>(true));
            _popups = new(GetComponentsInChildren<Popup>(true));
        }

    }

}
