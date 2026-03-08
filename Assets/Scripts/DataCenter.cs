using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter : MonoBehaviour, IDataCenter
{
    [SerializeField] private double[] levelTimes = new double[3];
    [SerializeField] private int currentLevel = 0;


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
