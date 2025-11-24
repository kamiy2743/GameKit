using System;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModal : UnityScreenNavigator.Runtime.Core.Modal.Modal, IUnityScreenNavigatorResource
    {
        [SerializeField] CanvasGroup canvasGroup;
        
        ModalId? id;

        void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        void Awake()
        {
            canvasGroup.alpha = 0;
        }

        public ModalId GetId()
        {
            return id ?? throw new InvalidOperationException("ModalIdが設定されていません。");
        }
        
        public void SetId(ModalId id)
        {
            this.id = id;
        }

        public virtual bool AllowUniversalPop()
        {
            return true;
        }
        
        IUnityScreenNavigatorResourceKey IUnityScreenNavigatorResource.GetResourceKey()
        {
            return ModalName.FromModalType(GetType());
        }
        
        GameObject IUnityScreenNavigatorResource.GetResource()
        {
            return gameObject;
        }
    }
}