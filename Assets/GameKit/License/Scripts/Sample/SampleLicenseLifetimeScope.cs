using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using UnityEngine;
using VContainer;

namespace GameKit.License.Sample
{
    public sealed class SampleLicenseLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] GameKitLicenseContentSetting licenseContentSetting;

        public override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
        
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<ILicenseContentValueProvider>(licenseContentSetting);
        }
    }
}