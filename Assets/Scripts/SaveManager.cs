using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{

    private IDataCenter _DataCenter;

    public void SetDependency(IDataCenter DataCenter)
    {
        _DataCenter = DataCenter;
    }

    
    [System.Serializable]
    public class SaveData
    {
        public double[] levelTimes = {9999.99, 9999.99, 9999.99};
        public int currentLevel = 1;
    }

    private SaveData GameData()
    {
        SaveData data = _DataCenter.CaptureData();
        
        return data;
    }

    private void LoadData(SaveData data)
    {
        _DataCenter.LoadData(data);
    }


    public void Save()
    {
        SaveData data = GameData();

        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, data);

        file.Close();
    }


    public void Load()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        SaveData data = (SaveData) formatter.Deserialize(file);

        LoadData(data);

        file.Close();
    }
}
