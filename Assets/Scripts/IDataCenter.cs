public interface IDataCenter
{
    double[] LevelTimes { get; set; }
    int CurrentLevel { get; set; }
  
    SaveManager.SaveData CaptureData();
    void LoadData(SaveManager.SaveData data);
}