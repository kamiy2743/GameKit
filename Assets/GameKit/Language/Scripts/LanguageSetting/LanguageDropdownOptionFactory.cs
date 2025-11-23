using System.Collections.Generic;
using System.Linq;
using GameKit.Localization;
using GameKit.UIComponent.Dropdown;
using R3;

namespace GameKit.Language.LanguageSetting
{
    public sealed class LanguageDropdownOptionFactory
    {
        readonly LocaleController localeController;

        public LanguageDropdownOptionFactory(LocaleController localeController)
        {
            this.localeController = localeController;
        }
        
        public DropdownOptionList Make()
        {
            var options = localeController.GetLocales().Select(x => x.Name).ToList();
            return new DropdownOptionList(new ReactiveProperty<List<string>>(options));
        }
    }
}