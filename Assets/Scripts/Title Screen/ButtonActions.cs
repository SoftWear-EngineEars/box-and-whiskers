using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void QuitGame() // Only works in editor, though we won't be building the game
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Scenes/Level1"); // Should change with the addition of other levels
    }
}
