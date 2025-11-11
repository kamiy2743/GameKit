using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniVRM10;

namespace GameKit.VRM
{
    public sealed class VRMLoader
    {
        public async UniTask<GameObject> LoadFromFileAsync(
            string path,
            RuntimeAnimatorController animatorController,
            CancellationToken ct
        )
        {
            var vrm = await Vrm10.LoadPathAsync(path, ct: ct);
            vrm.GetComponent<Animator>().runtimeAnimatorController = animatorController;
            return vrm.gameObject;
        }
    }
}