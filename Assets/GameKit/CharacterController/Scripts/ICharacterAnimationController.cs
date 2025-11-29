namespace GameKit.CharacterController
{
    public interface ICharacterAnimationController
    {
        void SetSpeed(float speed);
        void SetJump(bool jump);
        void SetGrounded(bool grounded);
        void SetFreeFall(bool freeFall);
    }
}