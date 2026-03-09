using System.Diagnostics;
using UnityEngine;

public abstract class WhiskersSpriteAnimation : SpriteAnimation
{

    public WhiskersSpriteAnimation(string animationName, Player player, int frameNumber) : base(animationName, player, frameNumber)
    {
    }

    public WhiskersSpriteAnimation(int frameCount, string animationName, Player player, int frameNumber) : base(frameCount, animationName, player, frameNumber)
    {
    }
    
    public override SpriteAnimation GetNextAnimation()
    {
        if (rigidbody.linearVelocity.y < 0)
        {
            HandleFall();
        }
        else if (rigidbody.linearVelocity.y > 0)
        {
            HandleRise();
        }
        else if (rigidbody.linearVelocity.x != 0)
        {
            HandleWalk();
        }
        else
        {
            HandleIdle();
        }

        return GetNextState();
    }

    public override SpriteAnimation GetNextState()
    {
        return nextState;
    }

    protected abstract void HandleFall();

    protected abstract void HandleRise();

    protected abstract void HandleWalk();

    protected abstract void HandleIdle();

    public abstract void HandleMerge();
}
