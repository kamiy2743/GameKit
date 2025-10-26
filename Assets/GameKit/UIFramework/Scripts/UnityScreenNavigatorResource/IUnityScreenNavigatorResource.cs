using UnityEngine;

namespace GameKit.UIFramework.UnityScreenNavigatorResource
{
    public interface IUnityScreenNavigatorResource
    {
        ResourceKey GetResourceKey();
        GameObject GetResource();
    }
}