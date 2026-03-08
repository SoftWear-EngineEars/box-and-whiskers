using UnityEngine;

public class KeyFollowState : KeyAndDoorState
{
    protected Whiskers Whiskers;

    protected SpriteRenderer SpriteRenderer;
    protected BoxCollider2D DoorCollider;
    
    private static readonly Sprite LockedSprite = Resources.Load<Sprite>("Sprites/Locked Door");

    public KeyFollowState(KeyAndDoor keyAndDoor, Whiskers whiskers) : base(keyAndDoor)
    {
        Whiskers = whiskers;

        SpriteRenderer = KeyAndDoor.GetDoor().GetComponent<SpriteRenderer>();
        DoorCollider = KeyAndDoor.GetDoor().GetComponent<BoxCollider2D>();
    }
    
    public override void Start()
    {
        // todo: stop idle animation here

        DoorCollider.enabled = true;
        SpriteRenderer.sprite = LockedSprite;
    }

    public override void Update()
    {
        const float acceptableDistance = 0.05f;
        const float offsetX = -0.9f;
        const float offsetY = 0.8f;
        const float speed = 1.2f;

        var key = KeyAndDoor.GetKey();
        
        var targetPosition =
            new Vector2(Whiskers.transform.position.x + offsetX, Whiskers.transform.position.y + offsetY);
        
        if (Vector2.Distance(targetPosition, key.transform.position) > acceptableDistance)
        {
            key.transform.position += ((Vector3)targetPosition - key.transform.position) * (speed * Time.deltaTime);
        }
    }
    public override void HandleCombinationEvent(CombinationEvent combinationEvent)
    {
        KeyAndDoor.SetState(new KeyFollowDoorOpenState(KeyAndDoor, Whiskers));
    }

    public override void HandleCollision(Collider2D collider)
    {
        // ignore
    }

}