using System.Collections.Generic;
using R3;

namespace GameKit.UIComponent.Dropdown
{
    public sealed record DropdownOptionList(ReadOnlyReactiveProperty<List<string>> Options)
    {
        public ReadOnlyReactiveProperty<List<string>> Options { get; } = Options;
    }
}