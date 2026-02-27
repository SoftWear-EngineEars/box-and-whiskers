public interface IState
{
    void HandleUp();
    void HandleHorizontal(float amount);
    void AdvanceState();
}
