using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.File;
using UnityEngine;
using UniVRM10;

namespace GameKit.VRM
{
    public sealed class VRMLoader
    {
        public async UniTask<GameObject> LoadFromFileAsync(
            FilePath path,
            RuntimeAnimatorController animatorController,
            CancellationToken ct
        )
        {
            var vrm = await Vrm10.LoadPathAsync(path.Value, ct: ct);
            vrm.GetComponent<Animator>().runtimeAnimatorController = animatorController;
            return vrm.gameObject;
        }
    }
}