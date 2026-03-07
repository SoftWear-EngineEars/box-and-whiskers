using System;
using UnityEngine;

public class NormalState : PlayerState
{
    public NormalState(Player player) : base(player) { }

    public override void Start()
    {
        Player.UpAction.Enable();
        Player.HorizontalAction.Disable();
    }
}