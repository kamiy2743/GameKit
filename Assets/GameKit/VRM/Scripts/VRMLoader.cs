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
            SetUpVRMInstance(vrm, animatorController);
            return vrm.gameObject;
        }
        
        void SetUpVRMInstance(Vrm10Instance vrmInstance, RuntimeAnimatorController animatorController)
        {
            var animator = vrmInstance.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            var vrmAnimatorController = vrmInstance.gameObject.AddComponent<VRMAnimatorController>();
            vrmAnimatorController.SetUp(animatorController);
        }
    }
}