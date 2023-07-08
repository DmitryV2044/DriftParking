using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        protected UIInteractor _interactor;

        [Inject]
        private void Init(UIInteractor interactor)
        {
            _interactor = interactor;
        }
    }
}

