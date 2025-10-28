using GameKit.DependencyInjection.Prefab;
using GameKit.DependencyInjection.Root;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.License
{
    public sealed class LicenseScrollViewLifetimeScope : BaseRootChildLifetimeScope
    {
        [SerializeField] LicenseScrollView licenseScrollView;
        [SerializeField] LicenseContent licenseContentPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(licenseScrollView);
            builder.RegisterPrefab(licenseContentPrefab);
            builder.RegisterEntryPoint<LicenseScrollViewPresenter>();
        }
    }
}