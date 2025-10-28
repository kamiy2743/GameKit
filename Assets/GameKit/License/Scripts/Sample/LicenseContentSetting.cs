using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameKit.License.Sample
{
    [CreateAssetMenu(fileName = "LicenseContentSetting", menuName = "GameKit/License/Sample/LicenseContentSetting")]
    public sealed class LicenseContentSetting : ScriptableObject, ILicenseContentValueProvider
    {
        [SerializeField] List<LicenseContent> licenseContents;
        
        IReadOnlyList<LicenseContentValue> ILicenseContentValueProvider.Get()
        {
            return licenseContents.Select(x => new LicenseContentValue(x.Name, x.Body)).ToList();
        }
        
        [Serializable]
        public sealed record LicenseContent
        {
            [SerializeField] string name;
            [SerializeField] string body;
            
            public string Name => name;
            public string Body => body;
        }
    }
}