using UnityEngine;

namespace Scripts.UI
{
    public abstract class Dialogue : UIElement
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);

        }
    }

}
