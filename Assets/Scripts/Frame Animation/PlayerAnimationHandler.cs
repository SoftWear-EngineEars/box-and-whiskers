using UnityEngine;

public abstract class PlayerAnimationHandler : MonoBehaviour, IAnimationFrameObserver
{
    public Player Player { get; private set; }
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    public void Initialize(Player player)
    {
        Player = player;
        _rigidbody = player.GetComponent<Rigidbody2D>();
        _renderer = player.GetComponent<SpriteRenderer>();

        GameObject.Find("Singleton").GetComponent<AnimationFrameManager>().SubscribeToAnimationFrame(this);
    }

    public virtual void HandleLeft()
    {
        _renderer.flipX = false;
    }

    public virtual void HandleRight()
    {
        _renderer.flipX = true;
    }

    public virtual void Update()
    {
        if (_rigidbody.linearVelocity.x < 0)
        {
            HandleLeft();
        }
        else if (_rigidbody.linearVelocity.x > 0)
        {
            HandleRight();
        }
    }

    public void OnAnimationFrame(int frame)
    {
        SpriteAnimation state = Player.GetAnimationState();

        state.OnAnimationFrame(frame);
        string spriteName = state.GetSprite();
        SetSprite(spriteName);
    }

    protected void SetSprite(string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>(spriteName);
        if (sprite != null)
        {
            _renderer.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite '{spriteName}' not found in Resources.");
        }
    }
}
