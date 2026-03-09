using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonActions : MonoBehaviour, IUsesDataCenter
{
    private IDataCenter _DataCenter;
    [SerializeField] private SaveManager saveManager;

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
        Time.timeScale = 1;
    }

    public void LoadFromSave()
    {
        saveManager.Load();
        SceneManager.LoadScene("Scenes/Level"+(_DataCenter.CurrentLevel));
        Time.timeScale = 1;
    }
}
