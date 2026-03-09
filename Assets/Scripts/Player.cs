using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour
{
    protected PlayerState State { get; private set; }
    protected SpriteAnimation AnimationState { get; private set; }
    protected PlayerAnimationHandler AnimationHandler { get; private set; }

    public InputAction UpAction { get; protected set; }
    public InputAction HorizontalAction { get; protected set;  }

    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;

    [SerializeField] private Transform groundChecker;

    private BoxCollider2D _collider;
    private LayerMask _jumpable;
    
    protected virtual void Start()
    {
        _jumpable = LayerMask.GetMask("Jumpable");
        _collider = GetComponent<BoxCollider2D>();
    }

    public virtual void Update()
    {
        State.Update();
    }

    public void SetState(PlayerState state)
    {
        State = state;
        state.Start();
    }

    public SpriteAnimation GetAnimationState()
    {
        return AnimationState;
    }

    public void SetAnimationHandler(PlayerAnimationHandler handler)
    {
        AnimationHandler = handler;
    }

    public void SetAnimationState(SpriteAnimation animation)
    {
        AnimationState = animation;
    }

    public float GetJumpStrength()
    {
        return jumpStrength;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public bool CanJump()
    {
        const float maxGroundDistance = 0.1f;
        
        var hit = Physics2D.BoxCast(groundChecker.position, new Vector2(_collider.size.x, maxGroundDistance), 0, Vector2.down, maxGroundDistance, _jumpable);
        return hit.collider != null && hit.collider.gameObject != gameObject;
    }
}
