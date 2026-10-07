using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistanceManager : MonoBehaviour
{
    [Header ("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistancesObjects;
    public static DataPersistanceManager instance { get; private set; }
    private FileDataHandler dataHandler;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistancesObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            NewGame();
        }

        foreach (IDataPersistance dataPersistanceObject in dataPersistancesObjects)
        {
            dataPersistanceObject.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistanceObject in dataPersistancesObjects)
        {
            dataPersistanceObject.SaveData(gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistancesObjects = FindObjectsByType(typeof(MonoBehaviour), FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistancesObjects);
    }
}
