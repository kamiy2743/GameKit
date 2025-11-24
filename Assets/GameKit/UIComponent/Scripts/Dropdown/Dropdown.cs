using System.Collections.Generic;
using System.Linq;
using R3;
using TMPro;
using UnityEngine;

namespace GameKit.UIComponent.Dropdown
{
    public sealed class Dropdown : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown dropdown;
        
        public void SetUp(DropdownOptionList optionList)
        {
            optionList.Options
                .Subscribe(UpdateOptions)
                .AddTo(this);
        }
        
        void UpdateOptions(IEnumerable<string> options)
        {
            dropdown.options.Clear();
            dropdown.options.AddRange(options.Select(x => new TMP_Dropdown.OptionData(x)));
            dropdown.RefreshShownValue();
        }
        
        public void SetValue(int value)
        {
            dropdown.value = value;
        }

        public Observable<int> OnValueChange()
        {
            return dropdown.OnValueChangedAsObservable();
        }
    }
}