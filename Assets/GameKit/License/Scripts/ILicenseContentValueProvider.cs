using System.Collections.Generic;

namespace GameKit.License
{
    public interface ILicenseContentValueProvider
    {
        IReadOnlyList<LicenseContentValue> Get();
    }
}