using UnityEngine;

namespace GameKit.CharacterController
{
    public sealed class JumpController : MonoBehaviour
    {
        [Header("Jump")]
        [SerializeField] float jumpSpeed = 5f;

        bool jumpRequested;
        bool hasLeftGroundSinceLastJump = true;

        public bool HasPendingJumpRequest => jumpRequested;

        public void RequestJump()
        {
            jumpRequested = true;
        }

        public void ResetJumpRequest()
        {
            jumpRequested = false;
        }

        public void NotifyGroundedState(bool grounded)
        {
            if (!grounded)
            {
                hasLeftGroundSinceLastJump = true;
            }
        }

        public void ApplyJump(ref Vector3 velocity, ref bool isGrounded)
        {
            if (!jumpRequested || !isGrounded || !hasLeftGroundSinceLastJump)
            {
                return;
            }

            var upward = Vector3.Dot(velocity, Vector3.up);
            if (upward < 0f)
            {
                velocity -= upward * Vector3.up;
            }

            velocity += Vector3.up * jumpSpeed;
            isGrounded = false;
            hasLeftGroundSinceLastJump = false;
        }
    }
}
