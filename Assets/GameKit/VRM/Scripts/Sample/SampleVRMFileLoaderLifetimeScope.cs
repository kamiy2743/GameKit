using System.Collections.Generic;
using GameKit.DependencyInjection.Root;
using UnityEngine;

namespace GameKit.VRM
{
    public sealed class SampleVRMFileLoaderLifetimeScope : BaseRootChildLifetimeScope
    {
        protected override void OnValidate()
        {
            base.OnValidate();
            
            autoInjectGameObjects = new List<GameObject>
            {
                gameObject,
            };
        }
    }
}