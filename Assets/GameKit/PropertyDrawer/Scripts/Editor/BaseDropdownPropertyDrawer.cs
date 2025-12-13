using System;
using System.Collections.Generic;
using System.Linq;
using GameKit.Collection;
using UnityEditor;
using UnityEngine;

namespace GameKit.PropertyDrawer.Editor
{
    public abstract class BaseDropdownPropertyDrawer : UnityEditor.PropertyDrawer
    {
        protected abstract IReadOnlyList<DropdownPropertyDrawerOption> GetOptions();

        protected virtual SerializedProperty ResolveValueProperty(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.Integer
                ? property
                : throw new InvalidOperationException("派生クラスでint型以外のプロパティの解決方法を実装してください");
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var scope = new EditorGUI.PropertyScope(position, label, property);
            
            var options = GetOptions();
            if (options.Count == 0)
            {
                EditorGUI.LabelField(position, label, new GUIContent("選択肢がありません"));
                return;
            }

            var valueProperty = ResolveValueProperty(property);
            if (valueProperty is not { propertyType: SerializedPropertyType.Integer })
            {
                throw new InvalidOperationException("int型以外のプロパティには対応していません");
            }

            var optionLabels = options.Select(x => x.Label).ToArray();
            var currentIndex = options.IndexOf(x => x.Value == valueProperty.intValue);
            if (currentIndex == null)
            {
                EditorGUI.LabelField(position, label, new GUIContent("不正な値です"));
                return;
            }
            var nextIndex = EditorGUI.Popup(position, label.text, currentIndex.Value, optionLabels);
            if (nextIndex >= 0 && nextIndex < options.Count && nextIndex != currentIndex)
            {
                valueProperty.intValue = options[nextIndex].Value;
            }
        }

        protected sealed record DropdownPropertyDrawerOption(string Label, int Value)
        {
            public string Label { get; } = Label;
            public int Value { get; } = Value;
        }
    }
}
