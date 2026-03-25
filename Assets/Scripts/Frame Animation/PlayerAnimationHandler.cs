using System.Diagnostics;
using UnityEngine;

public abstract class PlayerAnimationHandler : MonoBehaviour, IAnimationFrameObserver
{
    public Player Player { get; private set; }
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;

    public void Initialize(Player player)
    {
        var animationFrameManager = FindObjectOfType<AnimationFrameManager>();
        Initialize(player, animationFrameManager);
    }

    public void Initialize(Player player, IAnimationFrameSubject animationFrameSubject)
    {
        Player = player;
        _rigidbody = player.GetComponent<Rigidbody2D>();
        _renderer = player.GetComponent<SpriteRenderer>();

        if (animationFrameSubject != null)
        {
            animationFrameSubject.SubscribeToAnimationFrame(this);
        }
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

        UpdateSprite();
    }

    protected void UpdateSprite()
    {
        SpriteAnimation state = Player.GetAnimationState();

        string spriteName = state.GetSprite();

        Player.SetAnimationState(state.GetNextState());
        SetSprite(spriteName); 
    }

    public void OnAnimationFrame(int frame)
    {
        if (Player.GetAnimationState() != null)
        {
            Player.GetAnimationState().OnAnimationFrame(frame);
        }
        UpdateSprite();
    }

    protected void SetSprite(string spriteName)
    {
        int lastUnderscoreIndex = spriteName.LastIndexOf('_');
        string baseName = spriteName.Substring(0, lastUnderscoreIndex);

        Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/{baseName}");

        if (sprites.Length > 0)
        {
            Sprite sprite = System.Array.Find(sprites, s => s.name == spriteName);
            _renderer.sprite = sprite;
        }
        else
        {
            UnityEngine.Debug.LogWarning($"No sprites found in the sprite sheet '{baseName}.png'.");
        }
    }
}
