using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.QA
{
    public abstract class AbstractQAButton : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField] RectTransform _content = null;
        [SerializeField] Button _prefab = null;
        [SerializeField] string _displayName = null;

        protected Button _button = null;

        public virtual void Init()
        {
            var _createdButton = Instantiate(_prefab, _content);
            _createdButton.name = $"Button_{_displayName}";

            var _textObject = _createdButton.GetComponentInChildren<TextMeshProUGUI>();
            _textObject.name = ($"Text_{_displayName}");
            _textObject.SetText($"{_displayName}");

            _button = _createdButton;
            _button.onClick.AddListener(DoSomething);
        }

        public virtual void ResetValues()
        {
            _button.onClick.RemoveAllListeners();
        }

        public abstract void DoSomething();
    }
}
