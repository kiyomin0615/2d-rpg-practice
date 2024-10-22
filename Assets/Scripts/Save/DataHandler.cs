using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataHandler
{
    string path = "";
    string fileName = "";

    public DataHandler(string path, string fileName)
    {
        this.path = path;
        this.fileName = fileName;
    }

    public void Save(GameData gameData)
    {
        string fullPath = Path.Combine(this.path, this.fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string jsonData = JsonUtility.ToJson(gameData, true);
            using (FileStream fs = new FileStream(fullPath, FileMode.Create)) // Write
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(jsonData);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log($"Failed to save data: {e}");
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(this.path, this.fileName);
        GameData gameData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string jsonData = "";
                using (FileStream fs = new FileStream(fullPath, FileMode.Open)) // Read
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        jsonData = sr.ReadToEnd();
                    }
                }

                gameData = JsonUtility.FromJson<GameData>(jsonData);
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to load data: {e}");
            }
        }

        return gameData;
    }

    public void Delete()
    {
        string fullPath = Path.Combine(this.path, this.fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Debug.Log("Save file deleted.");
        }
    }
}
