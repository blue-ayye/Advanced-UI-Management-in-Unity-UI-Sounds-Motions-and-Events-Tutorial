using PrimeTween;
using System;
using UnityEngine;

namespace BluePixel.UISystem
{
    [RequireComponent(typeof(CanvasGroup), typeof(RectTransform))]
    public class UITransition : MonoBehaviour
    {
        public enum TransitionType
        {
            None,
            Fade,
            Zoom,
            SlideRight,
            SlideLeft,
            SlideUp,
            SlideDown
        }

        [SerializeField] private TransitionType _openingTransition;
        [SerializeField] private TransitionType _closingTransition;
        [SerializeField] private TweenSettings _openingTweenSettings;
        [SerializeField] private TweenSettings _closingTweenSettings;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        public void Initialize()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void PlayOpeningTransition(bool playTransition, Action onComplete)
        {
            ResetTransitionState();

            if (!playTransition || _openingTransition == TransitionType.None)
            {
                onComplete();
                return;
            }

            var tween = new Tween();

            switch (_openingTransition)
            {
                case TransitionType.Fade:
                   tween = Tween.Alpha(_canvasGroup, 0, 1, _openingTweenSettings);
                    break;
                case TransitionType.Zoom:
                   tween =  Tween.Scale(_rectTransform, Vector3.zero, Vector3.one, _openingTweenSettings);
                    break;
                case TransitionType.SlideRight:
                    tween = Tween.UIAnchoredPositionX(_rectTransform, -_rectTransform.rect.width, 0, _openingTweenSettings);
                    break;
                case TransitionType.SlideLeft:
                    tween = Tween.UIAnchoredPositionX(_rectTransform, _rectTransform.rect.width, 0, _openingTweenSettings);
                    break;
                case TransitionType.SlideUp:
                    tween = Tween.UIAnchoredPositionY(_rectTransform, -_rectTransform.rect.height, 0, _openingTweenSettings);
                    break;
                case TransitionType.SlideDown:
                    tween = Tween.UIAnchoredPositionY(_rectTransform, _rectTransform.rect.height, 0, _openingTweenSettings);
                    break;
            }

            tween.OnComplete(() =>
            {
                ResetTransitionState();
                onComplete();
            });
        }

        public void PlayClosingTransition(bool playTransition, Action onComplete)
        {
            ResetTransitionState();

            if (!playTransition || _openingTransition == TransitionType.None)
            {
                onComplete();
                return;
            }

            var tween = new Tween();

            switch (_closingTransition)
            {
                case TransitionType.Fade:
                    tween = Tween.Alpha(_canvasGroup, 1, 0, _closingTweenSettings);
                    break;
                case TransitionType.Zoom:
                    tween = Tween.Scale(_rectTransform, Vector3.one, Vector3.zero, _closingTweenSettings);
                    break;
                case TransitionType.SlideRight:
                    tween = Tween.UIAnchoredPositionX(_rectTransform, 0, _rectTransform.rect.width, _closingTweenSettings);
                    break;
                case TransitionType.SlideLeft:
                    tween = Tween.UIAnchoredPositionX(_rectTransform, 0, -_rectTransform.rect.width, _closingTweenSettings);
                    break;
                case TransitionType.SlideUp:
                    tween = Tween.UIAnchoredPositionY(_rectTransform, 0, _rectTransform.rect.height, _closingTweenSettings);
                    break;
                case TransitionType.SlideDown:
                    tween = Tween.UIAnchoredPositionY(_rectTransform, 0, -_rectTransform.rect.height, _closingTweenSettings);
                    break;
            }

            tween.OnComplete(() =>
            {
                ResetTransitionState();
                onComplete();
            });
        }

        private void ResetTransitionState()
        {
            Tween.StopAll(_rectTransform);
            Tween.StopAll(_canvasGroup);

            _canvasGroup.alpha = 1;
            _rectTransform.localScale = Vector3.one;
            _rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}