using System;
using GameKit.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace GameKit.UIComponent.Text
{
    public sealed class Text : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        [SerializeField] LocalizeStringEvent localizeStringEvent;
        [SerializeField] bool useLocalization;

        void OnValidate()
        {
            localizeStringEvent.enabled = useLocalization;
        }
        
        public void SetText(LocalizedString localizedString)
        {
            if (!useLocalization)
            {
                throw new InvalidOperationException("ローカライズが無効の場合、ローカライズテキストは設定できません");
            }
            localizeStringEvent.StringReference = localizedString;
        }

        public void SetPlainText(string value)
        {
            if (useLocalization)
            {
                throw new InvalidOperationException("ローカライズが有効の場合、プレーンテキストは設定できません");
            }
            text.text = value;
        }
    }
}