using UnityEngine;
using UnityEngine.InputSystem;

namespace GameKit.Camera.Sample
{
    public sealed class SimpleCameraInput : MonoBehaviour
    {
        [SerializeField] InputActionReference lookAction;

        ICamera? camera;

        void Start()
        {
            camera = GetComponent<ICamera>();
        }

        void Update()
        {
            var lookInput = lookAction.action.ReadValue<Vector2>();
            camera?.SetLookInput(lookInput);
        }
    }
}