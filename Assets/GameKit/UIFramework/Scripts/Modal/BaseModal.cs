using System;
using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModal : UnityScreenNavigator.Runtime.Core.Modal.Modal, IUnityScreenNavigatorResource
    {
        ModalId? id;
        
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