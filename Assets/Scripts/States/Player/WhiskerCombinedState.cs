using UnityEngine;

public class WhiskerCombinedState : WhiskerState, IDependency<INotifier<CombinationEvent>>
{
    private readonly Box _box;

    private readonly Sprite _combinedSprite = Resources.Load<Sprite>("Sprites/whiskers_in_box");
    private readonly Sprite _uncombinedSprite = Resources.Load<Sprite>("Sprites/whiskers_idle");

    private readonly SpriteRenderer _spriteRenderer;
    private readonly BoxCollider2D _collider;
    private readonly Rigidbody2D _whiskersRigidbody;

    private INotifier<CombinationEvent> _combinationEventNotifier;

    public WhiskerCombinedState(Whiskers whiskers, Box box) : base(whiskers)
    {
        _box = box;
        Rigidbody = _box.GetComponent<Rigidbody2D>();

        _spriteRenderer = Whiskers.GetComponent<SpriteRenderer>();
        _collider = Whiskers.GetComponent<BoxCollider2D>();
        _whiskersRigidbody = Whiskers.GetComponent<Rigidbody2D>();
        
        // need to set dependency since dependency injector can't inject at creation
        // done in constructor so that during testing it can be reset before calling Start()
        SetDependency(CombinationEventNotifier.Instance);
    }

    public override void Start()
    {
        const float offset = 0f;
        
        Whiskers.HorizontalAction.Disable();
        Whiskers.UpAction.Enable();

        _collider.enabled = false;
        _whiskersRigidbody.bodyType = RigidbodyType2D.Kinematic;
        _whiskersRigidbody.linearVelocity = Vector2.zero;
        _whiskersRigidbody.totalForce = Vector2.zero;
        
        Whiskers.transform.SetParent(_box.transform);
        Whiskers.transform.position = _box.transform.position + (Vector3.up * offset);
        _spriteRenderer.sprite = _combinedSprite;
        
        _combinationEventNotifier.NotifySubscribers(CombinationEvent.Combine);
    }
    
    public override void HandleShift()
    {
        _collider.enabled = true;
        _whiskersRigidbody.bodyType = RigidbodyType2D.Dynamic;
        
        Whiskers.transform.SetParent(Whiskers.transform.parent.parent);

        _spriteRenderer.sprite = _uncombinedSprite;
        
        Whiskers.SetState(new WhiskerNormalState(Whiskers));
        
        _combinationEventNotifier.NotifySubscribers(CombinationEvent.Uncombine);
    }

    public override void Jump()
    {
        if (_box.CanJump())
            Rigidbody.AddForce(new Vector2(0, _box.GetJumpStrength()), ForceMode2D.Impulse);
    }
    
    public void SetDependency(INotifier<CombinationEvent> dependency)
    {
        _combinationEventNotifier = dependency;
    }
}