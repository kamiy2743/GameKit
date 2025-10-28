using GameKit.DependencyInjection.Prefab;
using UnityEngine;
using VContainer.Unity;

namespace GameKit.License
{
    public sealed class LicenseScrollViewPresenter : IInitializable
    {
        readonly LicenseScrollView licenseScrollView;
        readonly PrefabFactory<LicenseContent> licenseContentFactory;

        public LicenseScrollViewPresenter(
            LicenseScrollView licenseScrollView,
            PrefabFactory<LicenseContent> licenseContentFactory
        )
        {
            this.licenseScrollView = licenseScrollView;
            this.licenseContentFactory = licenseContentFactory;
        }

        void IInitializable.Initialize()
        {
            licenseScrollView.Bind(
                () => licenseContentFactory.Make().gameObject,
                SetUpLicenseContent
            );
        }
        
        void SetUpLicenseContent(GameObject gameObject, int index)
        {
            gameObject.GetComponent<LicenseContent>().SetContent($"Name: {index}", $"This is the license content for item {index}.");
        }
    }
}