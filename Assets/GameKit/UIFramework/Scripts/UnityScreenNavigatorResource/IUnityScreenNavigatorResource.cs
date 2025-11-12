using UnityEngine;

namespace GameKit.UIFramework.UnityScreenNavigatorResource
{
    public interface IUnityScreenNavigatorResource
    {
        IUnityScreenNavigatorResourceKey GetResourceKey();
        GameObject GetResource();
    }
}