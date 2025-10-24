using UnityEngine;
using UnityEngine.InputSystem;

namespace GameKit.Camera.Sample
{
    public sealed class SimpleFirstPersonCameraInput : MonoBehaviour
    {
        [SerializeField] InputActionReference lookAction;
        [SerializeField] FirstPersonCamera firstPersonCamera;

        void Update()
        {
            var lookInput = lookAction.action.ReadValue<Vector2>();
            firstPersonCamera.SetLookInput(lookInput);
        }
    }
}