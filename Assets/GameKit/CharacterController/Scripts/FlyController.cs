using UnityEngine;

namespace GameKit.CharacterController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MovementController))]
    public sealed class FlyController : MonoBehaviour
    {
        [SerializeField] float maxFlySpeed = 6f;
        [SerializeField] float flyAcceleration = 75f;
        [SerializeField] float verticalSpeed = 6f;
        [SerializeField] float verticalAcceleration = 75f;

        Rigidbody rb;
        MovementController movementController;
        float verticalInput;

        public bool IsFlyMode { get; private set; }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            movementController = GetComponent<MovementController>();
        }

        public void SetFlyMode(bool enabled)
        {
            if (IsFlyMode == enabled)
            {
                return;
            }

            IsFlyMode = enabled;
            rb.useGravity = !enabled;

            if (enabled)
            {
                var velocity = rb.linearVelocity;
                rb.linearVelocity = new Vector3(velocity.x, 0f, velocity.z);
            }
            else
            {
                verticalInput = 0f;
            }
        }

        public void SetVerticalInput(float input)
        {
            verticalInput = Mathf.Clamp(input, -1f, 1f);
        }

        public Vector3 CalculateDesiredVelocity(float deltaTime)
        {
            var desiredPlanar = Vector3.zero;
            if (movementController.TryGetWorldMovementDirection(out var worldDirection))
            {
                desiredPlanar = worldDirection * maxFlySpeed;
            }

            var current = rb.linearVelocity;
            var currentPlanar = new Vector3(current.x, 0f, current.z);
            var planar = Vector3.MoveTowards(currentPlanar, desiredPlanar, flyAcceleration * deltaTime);

            var desiredVertical = verticalInput * verticalSpeed;
            var vertical = Mathf.MoveTowards(current.y, desiredVertical, verticalAcceleration * deltaTime);

            return new Vector3(planar.x, vertical, planar.z);
        }
    }
}
