using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;

namespace GameKit.License.Sample
{
    public sealed class SampleLicenseLifetimeScope : BaseRootMBLifetimeScopeRegistration
    {
        [SerializeField] GameKitLicenseContentSetting licenseContentSetting;
        
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<ILicenseContentValueProvider>(licenseContentSetting);
        }
    }
}