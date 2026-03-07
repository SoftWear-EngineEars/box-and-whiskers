public interface INotifier<T>
{
    void RegisterSubscriber(ISubscriber<T> subscriber);

    void NotifySubscribers(T message);
}