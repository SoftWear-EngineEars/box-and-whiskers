using System;
using UnityEngine;

public class NormalState : PlayerState, ICanMove, ICanJump
{
    public NormalState(Player player) : base(player) { }

    public IState JumpState()
    {
        return new JumpingState(Player);
    }

    public void AdvanceState()
    {
        // stay in Normal
    }
}