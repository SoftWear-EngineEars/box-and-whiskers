using System.Collections.Generic;
using UnityEngine;

public class CombinationEventNotifier : Singleton<CombinationEventNotifier>, INotifier<CombinationEvent>
{
    private readonly HashSet<ISubscriber<CombinationEvent>> _subscribers = new();
    
    public void RegisterSubscriber(ISubscriber<CombinationEvent> subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void NotifySubscribers(CombinationEvent message)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.ReceiveEvent(message);
        }
    }
}