using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(GroundController))]
    [RequireComponent(typeof(JumpController))]
    public sealed class AnimationController : MonoBehaviour
    {
        [SerializeField][Min(0f)] float freeFallVelocityThreshold = 0.1f;

        Rigidbody rb;
        GroundController groundController;
        JumpController jumpController;
        ICharacterAnimationController? animationController;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            groundController = GetComponent<GroundController>();
            jumpController = GetComponent<JumpController>();
        }

        public void SetAnimationController(ICharacterAnimationController? animationController)
        {
            this.animationController = animationController;
        }

        void Update()
        {
            if (animationController == null)
            {
                return;
            }

            UpdateAnimationParameters();
        }

        void UpdateAnimationParameters()
        {
            var velocity = rb.linearVelocity;
            var planarSpeed = new Vector3(velocity.x, 0f, velocity.z).magnitude;
            var grounded = groundController.IsGrounded;

            animationController!.SetSpeed(planarSpeed);
            animationController.SetGrounded(grounded);

            var verticalVelocity = velocity.y;
            var isJumping = jumpController.HasPendingJumpRequest;
            var isFreeFalling = !grounded && verticalVelocity < -freeFallVelocityThreshold;

            animationController.SetJump(isJumping);
            animationController.SetFreeFall(isFreeFalling);
        }
    }
}
