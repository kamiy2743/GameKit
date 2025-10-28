using System;
using GameKit.UIComponent.LoopScrollView;
using UnityEngine;

namespace GameKit.License
{
    public sealed class LicenseScrollView : MonoBehaviour
    {
        [SerializeField] LoopVerticalScrollView loopScrollView;

        public void Bind(
            Func<GameObject> makeDelegate,
            Action<GameObject, int> setUpDelegate
        )
        {
            loopScrollView.Bind(makeDelegate, setUpDelegate);
        }
    }
}