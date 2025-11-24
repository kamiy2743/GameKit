using System.Collections.Generic;
using R3;

namespace GameKit.UIComponent.Dropdown
{
    public sealed record DropdownOptionList(ReadOnlyReactiveProperty<IEnumerable<string>> Options)
    {
        public ReadOnlyReactiveProperty<IEnumerable<string>> Options { get; } = Options;
    }
}