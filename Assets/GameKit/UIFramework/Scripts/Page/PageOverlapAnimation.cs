using UnityScreenNavigator.Runtime.Core.Shared;

namespace GameKit.UIFramework.Page
{
    public sealed class PageOverlapAnimation : TransitionAnimationBehaviour
    {
        float duration;
        public override float Duration => duration;
        
        public void SetDuration(float duration)
        {
            this.duration = duration;
        }
        
        public override void Setup()
        {
        }

        public override void SetTime(float time)
        {
        }
    }
}