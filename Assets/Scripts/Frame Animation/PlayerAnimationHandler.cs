using System.Diagnostics;
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

        var animationFrameManager = FindObjectOfType<AnimationFrameManager>();
        if (animationFrameManager != null)
        {
            animationFrameManager.SubscribeToAnimationFrame(this);
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
    }

    public void OnAnimationFrame(int frame)
    {
        SpriteAnimation state = Player.GetAnimationState();

        state.OnAnimationFrame(frame);
        string spriteName = state.GetSprite();

        if (state is WhiskersMergeAnimation)
        {
            UnityEngine.Debug.Log("Handling merge animation frame");
            UnityEngine.Debug.Log("Next state: " + state.GetNextState().GetType().Name);
        }

        Player.SetAnimationState(state.GetNextState());
        SetSprite(spriteName);
    }

    protected void SetSprite(string spriteName)
    {
        // Extract the base name (e.g., 'whiskers_falling') from the spriteName
        int lastUnderscoreIndex = spriteName.LastIndexOf('_');
        string baseName = spriteName.Substring(0, lastUnderscoreIndex);

        // Load all sprites from the base sprite sheet
        Sprite[] sprites = Resources.LoadAll<Sprite>($"Sprites/{baseName}");

        if (sprites.Length > 0)
        {
            // Find the specific sprite by name (e.g., 'whiskers_falling_0')
            Sprite sprite = System.Array.Find(sprites, s => s.name == spriteName);

            if (sprite != null)
            {
                _renderer.sprite = sprite;
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Sprite '{spriteName}' not found in the sprite sheet '{baseName}.png'.");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning($"No sprites found in the sprite sheet '{baseName}.png'.");
        }
    }
}
