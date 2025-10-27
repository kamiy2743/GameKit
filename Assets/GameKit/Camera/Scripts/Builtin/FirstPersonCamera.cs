using Unity.Cinemachine;
using UnityEngine;

namespace GameKit.Camera.Builtin
{
    public sealed class FirstPersonCamera : MonoBehaviour, ICamera
    {
        [SerializeField] float accelTime;
        [SerializeField] float decelTime;
        [SerializeField] Vector2 pitchLimits = new(-80f, 80f);

        Vector2 input;
        Vector2 angularVelocity;
        float yaw;
        float pitch;

        void ICamera.SetEnable(bool enable)
        {
            gameObject.SetActive(enable);
        }

        void ICamera.SetLookInput(Vector2 input)
        {
            this.input = input;
        }

        void Update()
        {
            var dt = Time.deltaTime;
            angularVelocity = new Vector2(
                UpdateAxisVelocity(angularVelocity.x, input.x, dt),
                UpdateAxisVelocity(angularVelocity.y, input.y, dt)
            );

            yaw += angularVelocity.x;
            pitch += -angularVelocity.y;

            yaw = WrapAngle(yaw);
            pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        }

        float UpdateAxisVelocity(float current, float desired, float dt)
        {
            var dampTime = Mathf.Abs(desired) > Mathf.Abs(current) ? accelTime : decelTime;
            return current + Damper.Damp(desired - current, dampTime, dt);
        }

        static float WrapAngle(float angle)
        {
            angle %= 360f;
            if (angle < 0f)
            {
                angle += 360f;
            }
            return angle;
        }
    }
}