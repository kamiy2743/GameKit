using UnityEngine;
using UnityEngine.InputSystem;

namespace GameKit.Camera.Sample
{
    public sealed class SimpleInputAxisInput : MonoBehaviour
    {
        [SerializeField] InputActionReference lookAction;
        [SerializeField] InputAxisController inputAxisController;

        void Update()
        {
            var lookInput = lookAction.action.ReadValue<Vector2>();
            inputAxisController.SetLookInput(lookInput);
        }
    }
}