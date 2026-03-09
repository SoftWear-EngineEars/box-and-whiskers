using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : IUsesDataCenter
{
    private IDataCenter _DataCenter;

    public void SetDependency(IDataCenter DataCenter)
    {
        _DataCenter = DataCenter;
    }


    public void ChangeScenes()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        if (currentScene.buildIndex == 0) // level 1
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level2");
        }
        
        if (currentScene.buildIndex == 2) // level 2
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Level3");
        }
        
        if (currentScene.buildIndex == 3) // level 3
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/Win");
        }
    }
}