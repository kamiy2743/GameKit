using System.Collections.Generic;
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
        
        void UpdateOptions(List<string> options)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
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