public interface ISubscriber<T> : IDependency<INotifier<T>>
{
    INotifier<T> Notifier { get; set; }
    public void ReceiveEvent(T message);
    void IDependency<INotifier<T>>.SetDependency(INotifier<T> notifier)
    {
        Notifier = notifier;
    }
}