using UnityEngine;

namespace GameKit.CharacterController
{
    public sealed class GroundController : MonoBehaviour
    {
        [SerializeField] LayerMask groundLayers = ~0;
        [SerializeField] float groundCheckRadius = 0.4f;
        [SerializeField] float groundCheckDistance = 0.1f;
        [SerializeField][Range(0f, 89f)] float slopeLimit = 45f;

        public bool IsGrounded { get; private set; }
        public Vector3 GroundNormal { get; private set; } = Vector3.up;

        public void UpdateGroundState()
        {
            var origin = transform.position + Vector3.up * (groundCheckRadius + groundCheckDistance);
            var hitGround = Physics.SphereCast(
                origin,
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
                SetUngrounded();
                return;
            }

            SetGrounded(hit.normal);
        }

        void SetUngrounded()
        {
            IsGrounded = false;
            GroundNormal = Vector3.up;
        }

        void SetGrounded(in Vector3 normal)
        {
            IsGrounded = true;
            GroundNormal = normal;
        }
    }
}

