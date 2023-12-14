using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.QA
{
    public class QAPanel : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] CanvasGroup _content = null;
        [SerializeField] AbstractQAButton[] _buttons = null;

        [Title("// UI - Buttons")]
        [SerializeField] Button _openButton = null;
        [SerializeField] Button _closeButton = null;

        private void Start()
        {
            int _count = _buttons.Length;
            for (int i = 0; i < _count; i++)
                _buttons[i].Init();

            Hide();
        }

        private void OnDestroy()
        {
            int _count = _buttons.Length;
            for (int i = 0; i < _count; i++)
                _buttons[i].ResetValues();
        }

        private void OnEnable()
        {
            _openButton.onClick.AddListener(Show);
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void Show()
        {
            _content.alpha = 1;
            _content.interactable = true;
            _content.blocksRaycasts = true;
        }

        private void Hide()
        {
            _content.alpha = 0;
            _content.interactable = false;
            _content.blocksRaycasts = false;
        }
    }
}
