public interface ISubscriber<T>
{
    public void ReceiveEvent(T message);
}