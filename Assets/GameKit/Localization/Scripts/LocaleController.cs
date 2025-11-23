using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization.Settings;

namespace GameKit.Localization
{
    public sealed class LocaleController
    {
        public IReadOnlyList<Locale> GetLocales()
        {
            return LocalizationSettings.AvailableLocales.Locales
                .Select(x => new Locale(x.LocaleName, x.Identifier.Code))
                .ToList();
        }
    }
}