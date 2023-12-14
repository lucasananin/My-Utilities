using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class SmartStringLocalizer : MonoBehaviour
{
    [SerializeField] LocalizedString _locString = null;

    public LocalizedString LocString { get => _locString; set => _locString = value; }

    private void Start()
    {
        _locString.Arguments = new List<object>();
    }

    public void SetArg(object _value)
    {
        if (!_locString.Arguments.Contains(_value))
        {
            _locString.Arguments.Add(_value);
        }

        _locString.Arguments[0] = _value;
    }
}
