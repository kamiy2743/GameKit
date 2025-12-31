using System;
using System.Collections.Generic;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using UnityEngine;

namespace GameKit.VRM
{
    public sealed class SampleVRMFileLoaderLifetimeScope : BaseLifetimeScope
    {
        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
        
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