using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameKit.UIComponent.LoopScrollView
{
    public sealed class LoopVerticalScrollView : MonoBehaviour, LoopScrollPrefabSource, LoopScrollDataSource
    {
        [SerializeField] LoopScrollRect loopScrollRect;
        
        Func<GameObject>? makeDelegate;
        Action<GameObject, int>? setUpDelegate;

        public void Bind(
            Func<GameObject> makeDelegate,
            Action<GameObject, int> setUpDelegate,
            int itemCount = -1
        )
        {
            this.makeDelegate = makeDelegate;
            this.setUpDelegate = setUpDelegate;
            
            loopScrollRect.ClearCells();
            loopScrollRect.prefabSource = this;
            loopScrollRect.dataSource = this;
            loopScrollRect.totalCount = itemCount;
            loopScrollRect.RefillCells();
        }

        GameObject LoopScrollPrefabSource.GetObject(int index)
        {
            return makeDelegate!.Invoke();
        }

        void LoopScrollPrefabSource.ReturnObject(Transform transform)
        {
            Destroy(transform.gameObject);
        }

        void LoopScrollDataSource.ProvideData(Transform transform, int index)
        {
            setUpDelegate!.Invoke(transform.gameObject, index);
            LayoutRebuilder.ForceRebuildLayoutImmediate((transform as RectTransform)!);
        }
    }
}