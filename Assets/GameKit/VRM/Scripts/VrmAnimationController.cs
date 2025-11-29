using GameKit.CharacterController;
using UnityEngine;

namespace GameKit.VRM
{
    public sealed class VrmAnimationController : ICharacterAnimationController
    {
        static readonly int Speed = Animator.StringToHash(nameof(Speed));
        static readonly int Jump = Animator.StringToHash(nameof(Jump));
        static readonly int Grounded = Animator.StringToHash(nameof(Grounded));
        static readonly int FreeFall = Animator.StringToHash(nameof(FreeFall));

        readonly Animator animator;
        
        public VrmAnimationController(Animator animator)
        {
            this.animator = animator;
        }
        
        void ICharacterAnimationController.SetSpeed(float speed)
        {
            animator.SetFloat(Speed, speed);
        }
        
        void ICharacterAnimationController.SetJump(bool jump)
        {
            animator.SetBool(Jump, jump);
        }
        
        void ICharacterAnimationController.SetGrounded(bool grounded)
        {
            animator.SetBool(Grounded, grounded);
        }
        
        void ICharacterAnimationController.SetFreeFall(bool freeFall)
        {
            animator.SetBool(FreeFall, freeFall);
        }
    }
}