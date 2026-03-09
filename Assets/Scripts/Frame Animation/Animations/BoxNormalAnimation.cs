using UnityEngine;

public class BoxNormalAnimation : SpriteAnimation
{
    public BoxNormalAnimation(Player player, int frameNumber) : base("box_front", player, frameNumber)
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
