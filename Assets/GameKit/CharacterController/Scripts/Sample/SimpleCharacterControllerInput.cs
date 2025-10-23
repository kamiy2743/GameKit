using UnityEngine;
using UnityEngine.InputSystem;

namespace GameKit.CharacterController.Sample
{
    public sealed class SimpleCharacterControllerInput : MonoBehaviour
    {
        [SerializeField] InputActionReference moveAction;
        [SerializeField] InputActionReference jumpAction;
        [SerializeField] CharacterController characterController;

        void Update()
        {
            var moveInput = moveAction.action.ReadValue<Vector2>();
            characterController.Move(moveInput);

            if (jumpAction.action.IsPressed())
            {
                characterController.Jump();
            }
        }
    }
}