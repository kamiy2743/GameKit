using UnityEngine;
using UnityEngine.InputSystem;

namespace GameKit.Camera.Sample
{
    public sealed class SimpleThirdPersonCameraInput : MonoBehaviour
    {
        [SerializeField] InputActionReference lookAction;
        [SerializeField] ThirdPersonCamera thirdPersonCamera;

        void Update()
        {
            var lookInput = lookAction.action.ReadValue<Vector2>();
            thirdPersonCamera.SetLookInput(lookInput);
        }
    }
}