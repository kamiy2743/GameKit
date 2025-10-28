using GameKit.DependencyInjection;
using UnityEngine;
using VContainer;

namespace GameKit.License.Sample
{
    public sealed class LicenseContentSettingLifetimeScope : BaseMBLifetimeScopeRegistration
    {
        [SerializeField] LicenseContentSetting licenseContentSetting;
        
        public override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<ILicenseContentValueProvider>(licenseContentSetting);
        }
    }
}