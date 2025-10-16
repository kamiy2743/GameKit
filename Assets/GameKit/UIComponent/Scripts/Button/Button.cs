using R3;
using UnityEngine;

namespace GameKit.UIComponent.Button
{
    public sealed class Button : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Button button;

        public Observable<Unit> OnClick() => button.OnClickAsObservable();
    }
}