using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    GameData gameData;
    List<ISaveManager> saveManagers = new List<ISaveManager>();
    DataHandler dataHandler;

    public Checkpoint[] checkpoints;

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

        checkpoints = FindObjectsOfType<Checkpoint>();

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

        LoadCheckpoints();
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref this.gameData);
        }

        SaveCheckpoints();
        dataHandler.Save(this.gameData);

        Debug.Log("Game Saved.");
    }

    [ContextMenu("Delete Save File")]
    public void DeleteSaveData()
    {
        dataHandler = new DataHandler(Application.persistentDataPath, fileName);
        dataHandler.Delete();
    }

    public bool HasSaveData()
    {
        if (dataHandler.Load() != null)
            return true;
        else
            return false;
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

    void SaveCheckpoints()
    {
        gameData.checkpointDict.Clear();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            gameData.checkpointDict.Add(checkpoint.uid, checkpoint.isActive);
        }

        gameData.closestCheckpointUID = FindClosestCheckpoint().uid;
    }

    void LoadCheckpoints()
    {
        Debug.Log("Load Checkpoints");

        foreach (KeyValuePair<string, bool> pair in gameData.checkpointDict)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (checkpoint.uid == pair.Key && pair.Value == true)
                {
                    checkpoint.ActivateCheckpoint();
                }
            }
        }

        SpawnPlayerToCheckpoint();
    }

    Checkpoint FindClosestCheckpoint()
    {
        Checkpoint closestCheckpoint = null;

        float closestDist = Mathf.Infinity;

        foreach (Checkpoint checkpoint in checkpoints)
        {
            float dist = Vector2.Distance(PlayerManager.instance.transform.position, checkpoint.transform.position);
            if (dist < closestDist && checkpoint.isActive)
            {
                closestDist = dist;
                closestCheckpoint = checkpoint;
            }
        }

        return closestCheckpoint;
    }

    void SpawnPlayerToCheckpoint()
    {
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (gameData.closestCheckpointUID == checkpoint.uid)
            {
                PlayerManager.instance.player.transform.position = checkpoint.transform.position;
            }
        }
    }
}
