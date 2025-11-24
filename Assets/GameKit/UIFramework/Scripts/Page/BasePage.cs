using GameKit.UIFramework.UnityScreenNavigatorResource;
using UnityEngine;

namespace GameKit.UIFramework.Page
{
    public abstract class BasePage : UnityScreenNavigator.Runtime.Core.Page.Page, IUnityScreenNavigatorResource
    {
        [SerializeField] CanvasGroup canvasGroup;

        void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        
        void Awake()
        {
            canvasGroup.alpha = 0;
        }
        
        public virtual bool AllowUniversalPop()
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