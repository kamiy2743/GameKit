using UnityEngine;

namespace GameKit.CharacterController
{
    public sealed class JumpController : MonoBehaviour
    {
        [SerializeField] float jumpSpeed = 5f;
        [SerializeField][Min(0f)] float groundedJumpResetDelay = 0.2f;

        bool jumpRequested;
        bool hasLeftGroundSinceLastJump = true;
        bool isCurrentlyGrounded;
        float groundedResetTimer;

        public bool HasPendingJumpRequest => jumpRequested;

        void FixedUpdate()
        {
            TickGroundedResetTimer(Time.fixedDeltaTime);
        }

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
            isCurrentlyGrounded = grounded;
            if (!grounded)
            {
                hasLeftGroundSinceLastJump = true;
                groundedResetTimer = 0f;
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
            groundedResetTimer = groundedJumpResetDelay;
        }

        void TickGroundedResetTimer(float deltaTime)
        {
            if (!isCurrentlyGrounded || hasLeftGroundSinceLastJump)
            {
                return;
            }

            if (groundedJumpResetDelay <= 0f)
            {
                hasLeftGroundSinceLastJump = true;
                return;
            }

            groundedResetTimer -= deltaTime;
            if (groundedResetTimer <= 0f)
            {
                hasLeftGroundSinceLastJump = true;
                groundedResetTimer = 0f;
            }
        }
    }
}
