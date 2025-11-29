using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(GroundController))]
    [RequireComponent(typeof(AnimationController))]
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] bool enableDebugLogs;

        Rigidbody rb;
        JumpController jumpController;
        MovementController movementController;
        GroundController groundController;
        AnimationController animationController;
    
        bool isGrounded;
        Vector3 groundNormal;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jumpController = GetComponent<JumpController>();
            movementController = GetComponent<MovementController>();
            groundController = GetComponent<GroundController>();
            animationController = GetComponent<AnimationController>();

            ConfigureRigidbody();
        }
        
        public void SetAnimationController(ICharacterAnimationController animationController)
        {
            this.animationController.SetAnimationController(animationController);
        }

        public void Move(Vector2 input)
        {
            movementController.SetMoveInput(input);
        }

        public void Jump()
        {
            jumpController.RequestJump();
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;

            groundController.UpdateGroundState();
            isGrounded = groundController.IsGrounded;
            groundNormal = groundController.GroundNormal;
            jumpController.NotifyGroundedState(isGrounded);

            var desiredPlanarVelocity = movementController.CalculateDesiredPlanarVelocity(dt, isGrounded, groundNormal);

            var velocity = rb.linearVelocity;
            velocity.x = desiredPlanarVelocity.x;
            velocity.z = desiredPlanarVelocity.z;

            jumpController.ApplyJump(ref velocity, ref isGrounded);

            if (enableDebugLogs)
            {
                LogState(desiredPlanarVelocity, velocity);
            }

            rb.linearVelocity = velocity;
            jumpController.ResetJumpRequest();
        }

        void ConfigureRigidbody()
        {
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        void LogState(Vector3 desiredPlanarVelocity, Vector3 velocity)
        {
            Debug.Log(
                $"[CharacterController] position={transform.position} rotation={transform.rotation.eulerAngles} moveInput={movementController.MoveInput} desiredPlanarVelocity={desiredPlanarVelocity} velocity={velocity} isGrounded={isGrounded} groundNormal={groundNormal} jumpRequested={jumpController.HasPendingJumpRequest} rigidbodyVelocity={rb.linearVelocity}"
            );
        }
    }
}
