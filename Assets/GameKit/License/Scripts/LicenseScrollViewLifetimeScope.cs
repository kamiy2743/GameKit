using System;
using GameKit.DependencyInjection;
using GameKit.DependencyInjection.Base;
using GameKit.DependencyInjection.Prefab;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameKit.License
{
    public sealed class LicenseScrollViewLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] LicenseScrollView licenseScrollView;
        [SerializeField] LicenseContent licenseContentPrefab;

        protected override Type GetParentType()
        {
            return typeof(RootLifetimeScope);
        }
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(licenseScrollView);
            builder.RegisterPrefab(licenseContentPrefab);
            builder.RegisterEntryPoint<LicenseScrollViewPresenter>();
        }
    }
}