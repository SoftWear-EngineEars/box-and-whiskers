public interface ISubscriber<T> : IDependency<INotifier<T>>
{
    public void ReceiveEvent(T message);
}