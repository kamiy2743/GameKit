using UnityEngine;
using UnityEngine.InputSystem;

namespace GameKit.CharacterController.Sample
{
    public sealed class SimpleCharacterControllerInput : MonoBehaviour
    {
        [SerializeField] InputActionReference moveAction;
        [SerializeField] InputActionReference jumpAction;
        [SerializeField] InputActionReference flyToggleAction;
        [SerializeField] InputActionReference flyUpAction;
        [SerializeField] InputActionReference flyDownAction;
        [SerializeField] CharacterController characterController;

        void Update()
        {
            var moveInput = moveAction.action.ReadValue<Vector2>();
            characterController.Move(moveInput);

            if (flyToggleAction.action.WasPressedThisFrame())
            {
                characterController.ToggleFlyMode();
            }

            if (characterController.IsFlyMode)
            {
                var vertical = 0f;
                if (flyUpAction.action.IsPressed())
                {
                    vertical += 1f;
                }

                if (flyDownAction.action.IsPressed())
                {
                    vertical -= 1f;
                }

                characterController.Fly(vertical);
                return;
            }

            if (jumpAction.action.IsPressed())
            {
                characterController.Jump();
            }
        }
    }
}
