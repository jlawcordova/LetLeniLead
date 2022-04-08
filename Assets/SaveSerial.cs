using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSerial
{
    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); 
        FileStream file = File.Create(Application.persistentDataPath 
                    + "/LetLeniLead.dat"); 
        SaveData data = new SaveData();
        
        data.TotalGlobalScore = GameManager.Instance.TotalGlobalScore;
        data.TotalLevelScore = GameManager.Instance.TotalLevelScore;
        data.Level = LevelManager.Instance.Level;
        data.TotalRosas = GameManager.Instance.TotalRosas;
        data.Volunteers = GameManager.Instance.Volunteers;
        data.Times3Unlocked = GameManager.Instance.Times3Unlocked;

        bf.Serialize(file, data);
        file.Close();
    }

    public static void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath 
                    + "/LetLeniLead.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                    File.Open(Application.persistentDataPath 
                    + "/LetLeniLead.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            GameManager.Instance.TotalGlobalScore = data.TotalGlobalScore;
            GameManager.Instance.TotalLevelScore = data.TotalLevelScore;
            GameManager.Instance.TotalRosas = data.TotalRosas;
            LevelManager.Instance.Level = data.Level == 0 ? 1 : data.Level;
            GameManager.Instance.Volunteers = data.Volunteers;
            GameManager.Instance.Times3Unlocked = data.Times3Unlocked;
        }
        else
        {
            GameManager.Instance.TotalRosas = 0;
            GameManager.Instance.TotalGlobalScore = 0;
            GameManager.Instance.TotalLevelScore = 0;
            LevelManager.Instance.Level = 1;
        }
    }
}


[Serializable]
public class SaveData
{
    public int Level;
    public int TotalGlobalScore;
    public int TotalLevelScore;
    public int TotalRosas;
    public int Volunteers;
    public bool Times3Unlocked;
}