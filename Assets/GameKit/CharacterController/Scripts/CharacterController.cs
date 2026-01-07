using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(GroundController))]
    [RequireComponent(typeof(FlyController))]
    [RequireComponent(typeof(AnimationController))]
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] bool enableDebugLogs;

        Rigidbody rb;
        JumpController jumpController;
        MovementController movementController;
        GroundController groundController;
        FlyController flyController;
        AnimationController animationController;
    
        bool isGrounded;
        Vector3 groundNormal;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jumpController = GetComponent<JumpController>();
            movementController = GetComponent<MovementController>();
            groundController = GetComponent<GroundController>();
            flyController = GetComponent<FlyController>();
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

        public void SetFlyMode(bool enabled)
        {
            flyController.SetFlyMode(enabled);
            groundController.SetForceUngrounded(enabled);
        }

        public void ToggleFlyMode()
        {
            flyController.SetFlyMode(!flyController.IsFlyMode);
        }

        public void Fly(float verticalInput)
        {
            flyController.SetVerticalInput(verticalInput);
        }

        public bool IsFlyMode => flyController.IsFlyMode;

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;

            if (flyController.IsFlyMode)
            {
                isGrounded = false;
                groundNormal = Vector3.up;
                jumpController.NotifyGroundedState(false);

                var flyVelocity = flyController.CalculateDesiredVelocity(dt);

                if (enableDebugLogs)
                {
                    LogState(flyVelocity, flyVelocity);
                }

                rb.linearVelocity = flyVelocity;
                jumpController.ResetJumpRequest();
                return;
            }

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
