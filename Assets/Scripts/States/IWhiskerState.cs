public interface IWhiskerState : IState
{
    public void HandleShift()
    {
        var whiskers = (Whiskers)Player;
        
        whiskers.EnterBox();
        whiskers.SetState(new WhiskerCombinedState(whiskers));
    }
}