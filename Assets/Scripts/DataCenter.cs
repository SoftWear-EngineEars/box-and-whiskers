using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoBehaviour, IDataCenter
{
    [SerializeField] private int currentLevel = 1;


    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }


    public SaveManager.SaveData CaptureData()
    {
        SaveManager.SaveData data = new SaveManager.SaveData();

        data.currentLevel = CurrentLevel;

        return data;
    }


    public void LoadData(SaveManager.SaveData data)
    {
        CurrentLevel = data.currentLevel;
    }
}
