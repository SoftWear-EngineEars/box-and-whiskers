public interface IDataCenter
{
    int CurrentLevel { get; set; }
  
    SaveManager.SaveData CaptureData();
    void LoadData(SaveManager.SaveData data);
}