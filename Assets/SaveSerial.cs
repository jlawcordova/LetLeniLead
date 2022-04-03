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

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
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
            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }
}


[Serializable]
public class SaveData
{
    public int TotalGlobalScore;
    public int TotalLevelScore;
}