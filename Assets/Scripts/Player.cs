using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour
{
    protected PlayerState State { get; private set; }

    public InputAction UpAction { get; protected set; }
    public InputAction HorizontalAction { get; protected set;  }

    [SerializeField] private float jumpStrength;
    [SerializeField] private float movementSpeed;

    [SerializeField] private Transform groundChecker;

    [SerializeField] private SceneChanger sceneChanger;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is on the 'Win' layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Win"))
        {
            sceneChanger.ChangeScenes();
        }
    }
}
