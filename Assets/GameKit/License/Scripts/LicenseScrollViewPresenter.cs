using System.Collections.Generic;
using GameKit.DependencyInjection.Prefab;
using UnityEngine;
using VContainer.Unity;

namespace GameKit.License
{
    public sealed class LicenseScrollViewPresenter : IInitializable
    {
        readonly LicenseScrollView licenseScrollView;
        readonly PrefabFactory<LicenseContent> licenseContentFactory;
        readonly ILicenseContentValueProvider licenseContentValueProvider;

        IReadOnlyList<LicenseContentValue>? licenseContentValues;

        public LicenseScrollViewPresenter(
            LicenseScrollView licenseScrollView,
            PrefabFactory<LicenseContent> licenseContentFactory,
            ILicenseContentValueProvider licenseContentValueProvider
        )
        {
            this.licenseScrollView = licenseScrollView;
            this.licenseContentFactory = licenseContentFactory;
            this.licenseContentValueProvider = licenseContentValueProvider;
        }

        void IInitializable.Initialize()
        {
            licenseContentValues = licenseContentValueProvider.Get();
            
            licenseScrollView.Bind(
                () => licenseContentFactory.Make().gameObject,
                SetUpLicenseContent,
                licenseContentValues.Count
            );
        }
        
        void SetUpLicenseContent(GameObject gameObject, int index)
        {
            var licenseContentValue = licenseContentValues![index];
            gameObject.GetComponent<LicenseContent>().SetValue(licenseContentValue);
        }
    }
}