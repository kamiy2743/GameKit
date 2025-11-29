using UnityEngine;

namespace GameKit.CharacterController
{
    public sealed class AnimationController : MonoBehaviour
    {
        ICharacterAnimationController? animationController;
        
        public void SetAnimationController(ICharacterAnimationController? animationController)
        {
            this.animationController = animationController;
        }
    }
}