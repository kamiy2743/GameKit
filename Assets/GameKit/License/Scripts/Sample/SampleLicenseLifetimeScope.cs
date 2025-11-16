using GameKit.DependencyInjection;
using UnityEngine;
using VContainer;

namespace GameKit.License.Sample
{
    public sealed class SampleLicenseLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] GameKitLicenseContentSetting licenseContentSetting;
        
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<ILicenseContentValueProvider>(licenseContentSetting);
        }
    }
}