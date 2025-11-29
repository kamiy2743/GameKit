using UnityEngine;

namespace GameKit.VRM
{
    public sealed class VRMObject : MonoBehaviour
    {
        public VrmAnimationController AnimationController { get; private set; }
        
        public void SetUp(Animator animator)
        {
            AnimationController = new VrmAnimationController(animator);
        }
    }
}