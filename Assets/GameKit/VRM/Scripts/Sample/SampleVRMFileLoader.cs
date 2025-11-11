using UnityEngine;
using VContainer;

namespace GameKit.VRM
{
    [RequireComponent(typeof(SampleVRMFileLoaderLifetimeScope))]
    public sealed class SampleVRMFileLoader : MonoBehaviour
    {
        [SerializeField][TextArea] string path;
        [SerializeField] Transform parent;
        
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
            var vrm = await vrmLoader.LoadFromFileAsync(path, destroyCancellationToken);
            vrm.gameObject.transform.SetParent(parent, false);
        }
    }
}