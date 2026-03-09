using UnityEngine;

public interface AnimationFrameSubject
{
    public void SubscribeToAnimationFrame(AnimationFrameObserver observer);
    public void UnsubscribeFromAnimationFrame(AnimationFrameObserver observer);
}
