using UnityEngine;

namespace GameKit.CharacterController
{
    public sealed class JumpController : MonoBehaviour
    {
        [SerializeField] float jumpSpeed = 5f;
        [SerializeField][Min(0f)] float groundedJumpResetDelay = 0.1f;

        bool jumpRequested;
        bool hasFullyLandedSinceLastJump = true;
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
            var wasGrounded = isCurrentlyGrounded;
            isCurrentlyGrounded = grounded;
            if (!grounded)
            {
                hasFullyLandedSinceLastJump = false;
                hasLeftGroundSinceLastJump = true;
                groundedResetTimer = 0f;
                return;
            }

            if (!wasGrounded)
            {
                if (groundedJumpResetDelay <= 0f)
                {
                    hasFullyLandedSinceLastJump = true;
                    groundedResetTimer = 0f;
                    return;
                }

                hasFullyLandedSinceLastJump = false;
                groundedResetTimer = groundedJumpResetDelay;
                return;
            }

            if (!hasLeftGroundSinceLastJump)
            {
                hasLeftGroundSinceLastJump = true;
                hasFullyLandedSinceLastJump = true;
                groundedResetTimer = 0f;
            }
        }

        public void ApplyJump(ref Vector3 velocity, ref bool isGrounded)
        {
            if (!jumpRequested || !isGrounded || !hasFullyLandedSinceLastJump || !hasLeftGroundSinceLastJump)
            {
                return;
            }

            var upward = Vector3.Dot(velocity, Vector3.up);
            velocity -= upward * Vector3.up;

            velocity += Vector3.up * jumpSpeed;
            isGrounded = false;
            hasFullyLandedSinceLastJump = false;
            hasLeftGroundSinceLastJump = false;
            groundedResetTimer = 0f;
        }

        void TickGroundedResetTimer(float deltaTime)
        {
            if (!isCurrentlyGrounded || hasFullyLandedSinceLastJump || groundedResetTimer <= 0f)
            {
                return;
            }

            groundedResetTimer -= deltaTime;
            if (groundedResetTimer <= 0f)
            {
                hasFullyLandedSinceLastJump = true;
                groundedResetTimer = 0f;
            }
        }
    }
}
