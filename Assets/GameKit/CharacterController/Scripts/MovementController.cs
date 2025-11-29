using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class MovementController : MonoBehaviour
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float acceleration = 75f;
        [SerializeField] float airControlMultiplier = 0.5f;
        [SerializeField] float rotationSpeed = 720f;
        [SerializeField] Transform modelTransform;

        Rigidbody rb;
        Transform cameraTransform;
        Vector2 moveInput;

        public Vector2 MoveInput => moveInput;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            cameraTransform = Camera.main!.transform;
        }

        void Update()
        {
            UpdateModelRotation(Time.deltaTime);
        }

        public void SetMoveInput(Vector2 input)
        {
            moveInput = Vector2.ClampMagnitude(input, 1f);
        }

        public Vector3 CalculateDesiredPlanarVelocity(float deltaTime, bool isGrounded, Vector3 groundNormal)
        {
            var hasWorldInput = TryGetWorldMovementDirection(out var worldInput);
            if (!hasWorldInput)
            {
                worldInput = Vector3.zero;
            }

            var desiredVelocity = worldInput * maxSpeed;
            if (isGrounded)
            {
                desiredVelocity = Vector3.ProjectOnPlane(desiredVelocity, groundNormal);
            }

            var currentPlanar = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            var maxDelta = (isGrounded ? acceleration : acceleration * airControlMultiplier) * deltaTime;
            return Vector3.MoveTowards(currentPlanar, desiredVelocity, maxDelta);
        }

        bool TryGetWorldMovementDirection(out Vector3 worldDirection)
        {
            if (moveInput.sqrMagnitude <= Mathf.Epsilon)
            {
                worldDirection = Vector3.zero;
                return false;
            }

            var inputDirection = new Vector3(moveInput.x, 0f, moveInput.y);
            worldDirection = GetWorldMovementDirection(inputDirection);
            if (worldDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                worldDirection = Vector3.zero;
                return false;
            }

            return true;
        }

        Vector3 GetWorldMovementDirection(Vector3 inputDirection)
        {
            var forwardSource = cameraTransform != null ? cameraTransform.forward : transform.forward;
            var cameraForward = Vector3.ProjectOnPlane(forwardSource, Vector3.up);
            if (cameraForward.sqrMagnitude <= Mathf.Epsilon)
            {
                return transform.TransformDirection(inputDirection);
            }
            cameraForward.Normalize();

            var rightSource = cameraTransform != null ? cameraTransform.right : transform.right;
            var cameraRight = Vector3.ProjectOnPlane(rightSource, Vector3.up);
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

        void UpdateModelRotation(float deltaTime)
        {
            if (!TryGetWorldMovementDirection(out var worldDirection))
            {
                return;
            }

            var normalizedDirection = worldDirection.normalized;
            if (normalizedDirection.sqrMagnitude <= Mathf.Epsilon)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(normalizedDirection, Vector3.up);
            modelTransform.rotation = Quaternion.RotateTowards(modelTransform.rotation, targetRotation, rotationSpeed * deltaTime);
        }
    }
}
