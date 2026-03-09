// Tests by Jacob Korn
// Paragraph:
// I was, unfortunately, unable to figure out how to test the behavior of a state machine in a single suite. I don’t think it should be done at all, unless one would want to test the overarching behavior of, e.g., the key beginning to follow `Whiskers` when it is touched; in that case, it wouldn’t be a unit test. As a result, a complete set of test suites would need to cover each state. The `KeyAndDoor` tests, therefore, are solely made to ensure the relations between the `KeyAndDoor` with the states (and with the `CombinationEventNotifier`). I’m unsure as to whether it is the correct choice to test the behaviors of `Item` here; however, since no other subclasses of `Item` exist, it might as well be tested in `KeyAndDoor` (`Item` is separate in order to facilitate that in the future, but no opportunity to use it came up). I believe the tests speak for themselves: the state should start when it starts, update on update, and handle things when they occur; and the notifications should work properly. All of these are extremely necessary for the behavior of `KeyAndDoor`/`Item`, as otherwise nothing would run in the state machine. 

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class KeyAndDoorTests
{
    private GameObject keyAndDoorObject;
    private KeyAndDoor keyAndDoor;
    
    [SetUp]
    public void Setup()
    {
        keyAndDoorObject = new GameObject();
        keyAndDoor = keyAndDoorObject.AddComponent<KeyAndDoor>();
    }

    [Test]
    public void SetState_StartsState()
    {
        var state = Substitute.For<IItemState>();

        keyAndDoor.SetState(state);

        state.Received().Start();
    }

    [Test]
    public void State_Updates_OnUpdate()
    {
        var state = Substitute.For<IItemState>();
        keyAndDoor.SetState(state);
        
        keyAndDoor.Update();
        
        state.Received().Update();
    }

    [Test]
    public void State_HandlesCollider_WhenCollision()
    {
        var state = Substitute.For<IItemState>();
        keyAndDoor.SetState(state);

        var otherObject = new GameObject();
        var collider = otherObject.AddComponent<Collider2D>();
        
        keyAndDoor.OnTriggerEnter2D(collider);
        
        state.Received().HandleCollision(collider);
    }

    [Test]
    public void State_HandlesCombinationEvent_WhenReceives()
    {
        var state = Substitute.For<IKeyAndDoorState>();
        keyAndDoor.SetState(state);
        
        keyAndDoor.ReceiveEvent(CombinationEvent.Combine);
        
        state.Received().HandleCombinationEvent(CombinationEvent.Combine);
    }

    [Test]
    public void KeyAndDoor_SubscribesToDependency_WhenSet()
    {
        var combinationEventNotifier = Substitute.For<INotifier<CombinationEvent>>();
        
        keyAndDoor.SetDependency(combinationEventNotifier);
        
        combinationEventNotifier.Received().RegisterSubscriber(keyAndDoor);
    }
}
