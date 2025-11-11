using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniVRM10;

namespace GameKit.VRM
{
    public sealed class VRMLoader
    {
        public async UniTask<GameObject> LoadFromFileAsync(string path, CancellationToken ct = default)
        {
            var vrm = await Vrm10.LoadPathAsync(path, ct: ct);
            return vrm.gameObject;
        }
    }
}