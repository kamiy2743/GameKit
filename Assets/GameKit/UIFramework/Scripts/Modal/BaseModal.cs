using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModal : UnityScreenNavigator.Runtime.Core.Modal.Modal, IUnityScreenNavigatorResource
    {
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