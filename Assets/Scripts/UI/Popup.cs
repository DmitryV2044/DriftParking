using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.UI;
using DG.Tweening;
using System;

namespace Scripts.UI
{
    public abstract class Popup : UIElement
    {
        [SerializeField, Range(0f, 1f)] private float _punchScale = 1.2f;
        [SerializeField] private UiAnimationType _openAnimation;
        [SerializeField] private UiAnimationType _closeAnimation;

        private Tween _currentTween;

        public virtual void Open()
        {
            gameObject.SetActive(true);
            Toggle(_openAnimation);
        }

        public virtual void Close()
        {
            Toggle(_closeAnimation, () =>
            {
                gameObject.SetActive(false);
                transform.localScale = Vector3.one;
                _currentTween.Kill();
            });
        }

        private void Toggle(UiAnimationType type, Action callback = null)
        {
            switch (type)
            {
                case UiAnimationType.Punch:
                    _currentTween = transform.DOPunchScale(transform.localScale * _punchScale, 0.5f, 1, 0)
                        .OnComplete(() => callback?.Invoke()).OnPause(() => callback?.Invoke());

                    break;
                default:
                    callback?.Invoke();
                    break;
            }
        }
    }

}


