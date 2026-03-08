using System.Collections.Generic;

public class AbstractNotifier<T> : Singleton<AbstractNotifier<T>>, INotifier<T>
{
    private readonly HashSet<ISubscriber<T>> _subscribers = new();
    
    public void RegisterSubscriber(ISubscriber<T> subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void NotifySubscribers(T message)
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.ReceiveEvent(message);
        }
    }
}