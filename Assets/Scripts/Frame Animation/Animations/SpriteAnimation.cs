using System.Security;
using UnityEngine;

public abstract class SpriteAnimation
{
    private readonly int _frameCount;
    private readonly string _spriteName;
    protected int currentFrame;

    protected Player player;
    protected Rigidbody2D rigidbody;
    protected SpriteRenderer renderer;

    protected SpriteAnimation nextState;

    public SpriteAnimation(int frameCount, string spriteName, Player player, int frameNumber)
    {
        _frameCount = frameCount;
        _spriteName = spriteName;
        this.player = player;
        currentFrame = frameNumber % frameCount;

        rigidbody = player.GetComponent<Rigidbody2D>();
        renderer = player.GetComponent<SpriteRenderer>();
    }

    public SpriteAnimation(string spriteName, Player player, int frameNumber) : this(2, spriteName, player, frameNumber)
    {
        // Default to 2 frames if not specified
    }

    public void OnAnimationFrame(int frame)
    {
        currentFrame = frame % _frameCount;
    }

    public string GetSprite()
    {
        return $"{_spriteName}_{currentFrame}";
    }

    public abstract SpriteAnimation GetNextAnimation();

    public abstract SpriteAnimation GetNextState();
}
