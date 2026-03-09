using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour, IUsesDataCenter
{
    private IDataCenter _DataCenter;

    public void SetDependency(IDataCenter DataCenter)
    {
        _DataCenter = DataCenter;
    }

    public void QuitGame() // Only works in editor, though we won't be building the game
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Level1"); // Should change with the addition of other levels
    }

    public void LoadFromSave()
    {
        SceneManager.LoadScene("Scenes/Level"+(_DataCenter.CurrentLevel)); // Should change with the addition of other levels
    }
}
