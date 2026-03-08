using System;
using UnityEngine;

public class KeyAndDoor : Item, ISubscriber<CombinationEvent>
{
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject door;

    private void Start()
    {
        SetState(new KeyIdleState(this));
        
        CombinationEventNotifier.Instance.RegisterSubscriber(this);
    }

    public void ReceiveEvent(CombinationEvent message)
    {
        ((KeyAndDoorState)ItemState).HandleCombinationEvent(message);
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