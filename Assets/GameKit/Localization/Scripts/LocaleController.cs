using System;
using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine.Localization.Settings;
using UnityLocale = UnityEngine.Localization.Locale;

namespace GameKit.Localization
{
    public sealed class LocaleController
    {
        public Observable<Locale> OnLocaleChange()
        {
            return Observable.FromEvent<Action<UnityLocale>, UnityLocale>(
                    h => h,
                    h => LocalizationSettings.SelectedLocaleChanged += h,
                    h => LocalizationSettings.SelectedLocaleChanged -= h
                )
                .Select(Locale.FromUnityLocale);
        }
        
        public IReadOnlyList<Locale> GetLocales()
        {
            return LocalizationSettings.AvailableLocales.Locales
                .Select(Locale.FromUnityLocale)
                .ToList();
        }

        public void SetLocale(Locale locale)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale.Code);
        }
    }
}
