using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(JumpController))]
    [RequireComponent(typeof(MovementController))]
    public sealed class CharacterController : MonoBehaviour
    {
        [Header("Grounding")]
        [SerializeField] LayerMask groundLayers = ~0;
        [SerializeField] float groundCheckRadius = 0.4f;
        [SerializeField] float groundCheckDistance = 0.1f;
        [SerializeField][Range(0f, 89f)] float slopeLimit = 45f;

        [Header("Debug")]
        [SerializeField] bool enableDebugLogs;

        Rigidbody rb;
        JumpController jumpController;
        MovementController movementController;
        bool isGrounded;
        Vector3 groundNormal;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jumpController = GetComponent<JumpController>();
            movementController = GetComponent<MovementController>();
            ConfigureRigidbody();
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

            UpdateGroundState();

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

        void UpdateGroundState()
        {
            var hitGround = Physics.SphereCast(
                transform.position + Vector3.up * (groundCheckRadius + groundCheckDistance),
                groundCheckRadius,
                Vector3.down,
                out var hit,
                groundCheckRadius + groundCheckDistance,
                groundLayers,
                QueryTriggerInteraction.Ignore
            );

            if (!hitGround)
            {
                SetUngrounded();
                return;
            }

            var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle > slopeLimit)
            {
                return;
            }

            SetGrounded(hit.normal);
        }

        void SetUngrounded()
        {
            isGrounded = false;
            groundNormal = Vector3.up;
            jumpController.NotifyGroundedState(isGrounded);
        }

        void SetGrounded(in Vector3 normal)
        {
            isGrounded = true;
            groundNormal = normal;
            jumpController.NotifyGroundedState(isGrounded);
        }

        void LogState(Vector3 desiredPlanarVelocity, Vector3 velocity)
        {
            Debug.Log(
                $"[CharacterController] position={transform.position} rotation={transform.rotation.eulerAngles} moveInput={movementController.MoveInput} desiredPlanarVelocity={desiredPlanarVelocity} velocity={velocity} isGrounded={isGrounded} groundNormal={groundNormal} jumpRequested={jumpController.HasPendingJumpRequest} rigidbodyVelocity={rb.linearVelocity}"
            );
        }
    }
}

