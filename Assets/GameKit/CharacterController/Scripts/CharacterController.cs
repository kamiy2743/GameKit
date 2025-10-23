using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public sealed class CharacterController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float acceleration = 75f;
        [SerializeField] float airControlMultiplier = 0.5f;

        [Header("Jump")]
        [SerializeField] float jumpSpeed = 5f;

        [Header("Grounding")]
        [SerializeField] LayerMask groundLayers = ~0;
        [SerializeField] float groundCheckRadius = 0.4f;
        [SerializeField] float groundCheckDistance = 0.1f;
        [SerializeField][Range(0f, 89f)] float slopeLimit = 45f;

        [Header("Debug")]
        [SerializeField] bool enableDebugLogs;

        Rigidbody rb;
        Vector2 moveInput;
        bool jumpRequested;
        bool isGrounded;
        bool hasLeftGroundSinceLastJump = true;
        Vector3 groundNormal;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ConfigureRigidbody();
        }

        public void Move(Vector2 input)
        {
            moveInput = Vector2.ClampMagnitude(input, 1f);
        }

        public void Jump()
        {
            jumpRequested = true;
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;

            UpdateGroundState();

            var desiredPlanarVelocity = GetDesiredPlanarVelocity(dt);

            var velocity = rb.linearVelocity;
            velocity.x = desiredPlanarVelocity.x;
            velocity.z = desiredPlanarVelocity.z;

            ApplyJump(ref velocity);

            if (enableDebugLogs)
            {
                LogState(desiredPlanarVelocity, velocity);
            }

            rb.linearVelocity = velocity;
            jumpRequested = false;
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
                isGrounded = false;
                hasLeftGroundSinceLastJump = true;
                groundNormal = Vector3.up;
                return;
            }

            var slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle > slopeLimit)
            {
                return;
            }

            isGrounded = true;
            groundNormal = hit.normal;
        }

        Vector3 GetDesiredPlanarVelocity(float deltaTime)
        {
            var inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            var worldInput = transform.TransformDirection(inputDirection);
            var desiredVelocity = worldInput * maxSpeed;

            if (isGrounded)
            {
                desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, groundNormal);
            }

            var currentPlanar = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            var maxDelta = (isGrounded ? acceleration : acceleration * airControlMultiplier) * deltaTime;
            return Vector3.MoveTowards(currentPlanar, desiredVelocity, maxDelta);
        }

        void ApplyJump(ref Vector3 velocity)
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

        void LogState(Vector3 desiredPlanarVelocity, Vector3 velocity)
        {
            Debug.Log(
                $"[PhysicsCharacterController] position={transform.position} rotation={transform.rotation.eulerAngles} moveInput={moveInput} desiredPlanarVelocity={desiredPlanarVelocity} velocity={velocity} isGrounded={isGrounded} groundNormal={groundNormal} jumpRequested={jumpRequested} rigidbodyVelocity={rb.linearVelocity}"
            );
        }
    }
}

