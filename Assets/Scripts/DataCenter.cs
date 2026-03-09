using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoBehaviour, IDataCenter
{
    [SerializeField] private double[] levelTimes = {9999.99, 9999.99, 9999.99};
    [SerializeField] private int currentLevel = 1;


    public double[] LevelTimes { get => levelTimes; set => levelTimes = value; }
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }


    public SaveManager.SaveData CaptureData()
    {
        SaveManager.SaveData data = new SaveManager.SaveData();

        data.levelTimes = LevelTimes;
        data.currentLevel = CurrentLevel;

        return data;
    }


    public void LoadData(SaveManager.SaveData data)
    {
        LevelTimes = data.levelTimes;
        CurrentLevel = data.currentLevel;
    }
}
