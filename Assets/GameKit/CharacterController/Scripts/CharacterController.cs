using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(JumpController))]
    public sealed class CharacterController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float acceleration = 75f;
        [SerializeField] float airControlMultiplier = 0.5f;

        [Header("Rotation")]
        [SerializeField] float rotationSpeed = 720f;
        [SerializeField] Transform modelTransform;

        [Header("Grounding")]
        [SerializeField] LayerMask groundLayers = ~0;
        [SerializeField] float groundCheckRadius = 0.4f;
        [SerializeField] float groundCheckDistance = 0.1f;
        [SerializeField][Range(0f, 89f)] float slopeLimit = 45f;

        [Header("Debug")]
        [SerializeField] bool enableDebugLogs;

        Rigidbody rb;
        JumpController jumpController;
        Transform cameraTransform;
        Vector2 moveInput;
        bool isGrounded;
        Vector3 groundNormal;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            jumpController = GetComponent<JumpController>();
            cameraTransform = Camera.main!.transform;
            ConfigureRigidbody();
        }

        public void Move(Vector2 input)
        {
            moveInput = Vector2.ClampMagnitude(input, 1f);
        }

        public void Jump()
        {
            jumpController.RequestJump();
        }

        void Update()
        {
            UpdateFacingDirection(Time.deltaTime);
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;

            UpdateGroundState();

            var desiredPlanarVelocity = GetDesiredPlanarVelocity(dt);

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

        Vector3 GetDesiredPlanarVelocity(float deltaTime)
        {
            var inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            var worldInput = GetWorldMovementDirection(inputDirection);
            var desiredVelocity = worldInput * maxSpeed;

            if (isGrounded)
            {
                desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, groundNormal);
            }

            var currentPlanar = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            var maxDelta = (isGrounded ? acceleration : acceleration * airControlMultiplier) * deltaTime;
            return Vector3.MoveTowards(currentPlanar, desiredVelocity, maxDelta);
        }

        Vector3 GetWorldMovementDirection(Vector3 inputDirection)
        {
            var cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
            if (cameraForward.sqrMagnitude <= Mathf.Epsilon)
            {
                return transform.TransformDirection(inputDirection);
            }
            cameraForward.Normalize();

            var cameraRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up);
            if (cameraRight.sqrMagnitude <= Mathf.Epsilon)
            {
                cameraRight = new Vector3(cameraForward.z, 0f, -cameraForward.x);
            }
            cameraRight.Normalize();

            var world = cameraRight * inputDirection.x + cameraForward * inputDirection.z;
            if (world.sqrMagnitude <= Mathf.Epsilon)
            {
                return Vector3.zero;
            }

            return world.normalized * inputDirection.magnitude;
        }

        void UpdateFacingDirection(float deltaTime)
        {
            if (moveInput.sqrMagnitude <= Mathf.Epsilon)
            {
                return;
            }

            var inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            var worldDirection = GetWorldMovementDirection(inputDirection);
            if (worldDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(worldDirection.normalized, Vector3.up);
            modelTransform.rotation = Quaternion.RotateTowards(modelTransform.rotation, targetRotation, rotationSpeed * deltaTime);
        }

        void LogState(Vector3 desiredPlanarVelocity, Vector3 velocity)
        {
            Debug.Log(
                $"[CharacterController] position={transform.position} rotation={transform.rotation.eulerAngles} moveInput={moveInput} desiredPlanarVelocity={desiredPlanarVelocity} velocity={velocity} isGrounded={isGrounded} groundNormal={groundNormal} jumpRequested={jumpController.HasPendingJumpRequest} rigidbodyVelocity={rb.linearVelocity}"
            );
        }
    }
}

