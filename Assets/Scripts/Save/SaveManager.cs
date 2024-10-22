using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    GameData gameData;
    List<ISaveManager> saveManagers = new List<ISaveManager>();
    DataHandler dataHandler;

    [SerializeField] string fileName;

    void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    void Start()
    {
        saveManagers = FindAllSaveManagers();
        dataHandler = new DataHandler(Application.persistentDataPath, fileName);

        LoadGame();
    }

    public void StartNewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(this.gameData);
        }

        if (this.gameData == null)
        {
            Debug.Log("No Game Data");
            StartNewGame();
        }
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref this.gameData);
        }

        dataHandler.Save(this.gameData);

        Debug.Log("Game Saved.");
    }


    void OnApplicationQuit()
    {
        SaveGame();
    }

    List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>(); // ?
        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete Save File")]
    void DeleteSaveData()
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName);
        dataHandler.Delete();
    }
}
