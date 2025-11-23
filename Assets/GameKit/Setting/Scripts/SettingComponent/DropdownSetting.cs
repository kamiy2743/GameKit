using System;
using System.Collections.Generic;
using GameKit.Collection;
using GameKit.Setting.SettingValue;
using GameKit.UIComponent.Dropdown;
using R3;
using UnityEngine;

namespace GameKit.Setting.SettingComponent
{
    public sealed class DropdownSetting : MonoBehaviour
    {
        [SerializeField] Dropdown dropdown;
        
        public void SetUp(DropdownOptionList optionList)
        {
            dropdown.SetUp(optionList);
        }
        
        public ISettingBindable<TDropdownSettingValue> MakeBindable<TDropdownSettingValue, TValue>(
            IReadOnlyList<TValue> optionValues,
            Func<TValue, TDropdownSettingValue> settingValueFactory
        )
            where TDropdownSettingValue : IDropdownSettingValue<TValue>
            where TValue : IEquatable<TValue>
        {
            return new DropdownSettingBindable<TDropdownSettingValue, TValue>(
                dropdown,
                optionValues,
                settingValueFactory
            );
        }
        
        sealed class DropdownSettingBindable<TDropdownSettingValue, TValue> : ISettingBindable<TDropdownSettingValue>
            where TDropdownSettingValue : IDropdownSettingValue<TValue>
            where TValue : IEquatable<TValue>
        {
            readonly Dropdown dropdown;
            readonly IReadOnlyList<TValue> optionValues;
            readonly Func<TValue, TDropdownSettingValue> settingValueFactory;

            public DropdownSettingBindable(
                Dropdown dropdown,
                IReadOnlyList<TValue> optionValues,
                Func<TValue, TDropdownSettingValue> settingValueFactory
            )
            {
                this.dropdown = dropdown;
                this.optionValues = optionValues;
                this.settingValueFactory = settingValueFactory;
            }
            
            void ISettingBindable<TDropdownSettingValue>.SetValue(TDropdownSettingValue value)
            {
                var index = optionValues.IndexOf(value.Value);
                if (index == null)
                {
                    throw new ArgumentException($"{value.Value}が選択肢に存在しません。");
                }
                dropdown.SetValue(index.Value);
            }

            Observable<TDropdownSettingValue> ISettingBindable<TDropdownSettingValue>.OnValueChange()
            {
                return dropdown.OnValueChange()
                    .Select(index => settingValueFactory.Invoke(optionValues[index]));
            }
        }
    }
}