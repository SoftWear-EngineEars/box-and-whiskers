using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour, IUsesDataCenter
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


    private IDataCenter _DataCenter;

    public void SetDependency(IDataCenter DataCenter)
    {
        _DataCenter = DataCenter;
    }
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object is on the 'Win' layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Win"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
        
            if (currentScene.buildIndex == 0) // level 1
            {
                _DataCenter.CurrentLevel = 2;
                _DataCenter.CaptureData();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level2");
            }
            
            if (currentScene.buildIndex == 2) // level 2
            {
                _DataCenter.CurrentLevel = 3;
                _DataCenter.CaptureData();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level3");
            }
            
            if (currentScene.buildIndex == 3) // level 3
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/WinScreen");
            }
        }
    }
}
