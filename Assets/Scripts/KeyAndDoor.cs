using System;
using UnityEngine;

public class KeyAndDoor : Item, ISubscriber<CombinationEvent>
{
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject door;

    public void SetDependency(INotifier<CombinationEvent> dependency)
    {
        dependency.RegisterSubscriber(this);
    }

    public void Start()
    {
        SetState(new KeyIdleState(this));
    }

    public void ReceiveEvent(CombinationEvent message)
    {
        ((IKeyAndDoorState)ItemState).HandleCombinationEvent(message);
    }

    public GameObject GetKey()
    {
        return key;
    }

    public GameObject GetDoor()
    {
        return door;
    }
}