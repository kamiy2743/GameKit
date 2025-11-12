using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.Modal
{
    public abstract class BaseModal : UnityScreenNavigator.Runtime.Core.Modal.Modal, IUnityScreenNavigatorResource
    {
        ResourceKey IUnityScreenNavigatorResource.GetResourceKey()
        {
            return ResourceKey.FromType(GetType());
        }
        
        GameObject IUnityScreenNavigatorResource.GetResource()
        {
            return gameObject;
        }
    }
}