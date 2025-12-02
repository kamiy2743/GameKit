using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameKit.File;
using UnityEngine;
using UniVRM10;

namespace GameKit.VRM
{
    public sealed class VRMLoader
    {
        public async UniTask<VRMObject> LoadFromFileAsync(
            FilePath path,
            RuntimeAnimatorController animatorController,
            CancellationToken ct
        )
        {
            try
            {
                var vrm = await Vrm10.LoadPathAsync(path.Value, ct: ct);
                return SetUpVRMInstance(vrm, animatorController);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (System.Exception e)
            {
                throw new LocalVRMFailedException(e);
            }
        }
        
        VRMObject SetUpVRMInstance(Vrm10Instance vrmInstance, RuntimeAnimatorController animatorController)
        {
            var animator = vrmInstance.GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            var vrmObject = vrmInstance.gameObject.AddComponent<VRMObject>();
            vrmObject.SetUp(animator);
            return vrmObject;
        }
    }
}