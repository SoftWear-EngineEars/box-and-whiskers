public interface IState
{
    Player Player { get; }
    void HandleUp();
    void HandleHorizontal(float amount);
}
