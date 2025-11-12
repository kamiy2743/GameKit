using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePage : UnityScreenNavigator.Runtime.Core.Page.Page, IUnityScreenNavigatorResource
    {
        public virtual bool AllowUniversalClose()
        {
            return true;
        }
        
        IUnityScreenNavigatorResourceKey IUnityScreenNavigatorResource.GetResourceKey()
        {
            return PageName.FromPageType(GetType());
        }
        
        GameObject IUnityScreenNavigatorResource.GetResource()
        {
            return gameObject;
        }
    }
}