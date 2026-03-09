using UnityEngine;

public class BoxNormalAnimation : SpriteAnimation
{
    public BoxNormalAnimation(Player player, int frameNumber) : base("box_normal", player, frameNumber)
    {
    }

    public override SpriteAnimation GetNextAnimation()
    {
        return GetNextState();
    }

    public override SpriteAnimation GetNextState()
    {
        return this;
    }
}
