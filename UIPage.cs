using System;
using UnityEngine;
using UnityEngine.Events;

namespace BluePixel.UISystem
{
    [RequireComponent(typeof(UIAudio), typeof(UITransition))]
    public class UIPage : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _disableWhenNextPageOpens;

        [Header("Events")]
        [SerializeField] private UnityEvent _afterInitialized;
        [SerializeField] private UnityEvent _beforeOpened;
        [SerializeField] private UnityEvent _afterOpened;
        [SerializeField] private UnityEvent _beforeClosed;
        [SerializeField] private UnityEvent _afterClosed;

        public bool DisableWhenNextPageOpens => _disableWhenNextPageOpens;

        private UIAudio _uiAudio;
        private UITransition _uiTransition;

        public void Initialize()
        {
            _uiAudio = GetComponent<UIAudio>();
            _uiTransition = GetComponent<UITransition>();

            _uiTransition.Initialize();

            _afterInitialized.Invoke();
        }

        public void Open(bool playAudio = false, bool playTransition = false)
        {
            _beforeOpened.Invoke();
            gameObject.SetActive(true);

            _uiAudio.PlayOpeningSound(playAudio);
            _uiTransition.PlayOpeningTransition(playTransition, () =>
            {
                _afterOpened.Invoke();
                //Debug.Log($"{gameObject.name} is opened");
            });
        }

        public void Close(bool playAudio = false, bool playTransition = false)
        {
            _beforeClosed.Invoke();
            _uiAudio.PlayClosingSound(playAudio);
            _uiTransition.PlayClosingTransition(playTransition, () =>
            {
                gameObject.SetActive(false);
                _afterClosed.Invoke();
                //Debug.Log($"{gameObject.name} is closed");
            });

        }
    }
}