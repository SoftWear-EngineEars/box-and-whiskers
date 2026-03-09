using UnityEngine;

public interface IAnimationFrameSubject
{
    public void SubscribeToAnimationFrame(IAnimationFrameObserver observer);
    public void UnsubscribeFromAnimationFrame(IAnimationFrameObserver observer);
}
