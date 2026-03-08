using UnityEngine;

public class KeyFollowDoorOpenState : KeyFollowState
{
    public KeyFollowDoorOpenState(KeyAndDoor keyAndDoor, Whiskers whiskers) : base(keyAndDoor, whiskers) { }
    
    private static readonly Sprite UnlockedSprite = Resources.Load<Sprite>("Sprites/Door");

    public override void Start()
    {
        DoorCollider.enabled = false;
        SpriteRenderer.sprite = UnlockedSprite;
    }

    public override void HandleCombinationEvent(CombinationEvent combinationEvent)
    {
        KeyAndDoor.SetState(new KeyFollowState(KeyAndDoor, Whiskers));
    }
}