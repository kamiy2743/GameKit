using System;
using GameKit.File;
using UnityEngine;
using VContainer;

namespace GameKit.VRM
{
    [RequireComponent(typeof(SampleVRMFileLoaderLifetimeScope))]
    public sealed class SampleVRMFileLoader : MonoBehaviour
    {
        [SerializeField][TextArea] string path;
        [SerializeField] Transform parent;
        [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] CharacterController.CharacterController? characterController;
        
        VRMLoader vrmLoader;

        [Inject]
        void Construct(VRMLoader vrmLoader)
        {
            this.vrmLoader = vrmLoader;
        }

        void OnValidate()
        {
            parent = transform;
        }

        async void Start()
        {
            try
            {
                var vrm = await vrmLoader.LoadFromFileAsync(new FilePath(path), animatorController, destroyCancellationToken);
                vrm.gameObject.transform.SetParent(parent, false);
                characterController?.SetAnimationController(vrm.AnimationController);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}