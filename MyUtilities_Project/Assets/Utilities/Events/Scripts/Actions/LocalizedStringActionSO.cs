using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace Utilities.Events
{
    [CreateAssetMenu(fileName = "OnLocalizedString", menuName = "SO/Events/Localized String Action", order = 200)]
    public class LocalizedStringActionSO : AbstractActionSO1<LocalizedString> { }
}
