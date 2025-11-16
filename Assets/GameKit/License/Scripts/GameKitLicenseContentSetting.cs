using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameKit.License
{
    [CreateAssetMenu(fileName = "GameKitLicenseContentSetting", menuName = "GameKit/License/GameKitLicenseContentSetting")]
    public sealed class GameKitLicenseContentSetting : ScriptableObject, ILicenseContentValueProvider
    {
        [SerializeField] LicenseContent[] licenseContents;
        
        IReadOnlyList<LicenseContentValue> ILicenseContentValueProvider.Get()
        {
            return licenseContents.Select(x => new LicenseContentValue(x.Name, x.Body)).ToArray();
        }
        
        [Serializable]
        public sealed record LicenseContent
        {
            [SerializeField] string name;
            [SerializeField][TextArea] string body;
            
            public string Name => name;
            public string Body => body;
        }
    }
}