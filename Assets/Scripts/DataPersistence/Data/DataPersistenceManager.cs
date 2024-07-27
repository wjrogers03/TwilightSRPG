using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using TMPro;


public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] bool useEncyprtion;
    [SerializeField] bool OverwriteData;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;


    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }


    #region lifecycle functions
    private void Awake()
    {
        
        
        if (instance != null)
        {
            Debug.LogWarning("DataPersistenceManager: Found more than one DPM in scene, destroying newest object.");
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(this.gameObject);


        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncyprtion);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //SceneManager.sceneUnloaded += OnSceneUnloaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnApplicationQuit()
    {
        //SaveGame();
    }


    public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        // I don't want to do this on every load, it overwrites stuff from memory.
        // this should be done very intentionally.
        if (GameDataManager.instance.previous_scene == "title")
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();
            LoadGame();
        }
        return;
    }

    public void OnSceneUnloaded(UnityEngine.SceneManagement.Scene scene)
    {
        //SaveGame(); // saving should be handled by calling a DPM.instance.SaveGame() call prior to scene changes.  I added it to the GSM.
    }

    #endregion


    #region Actions
    public void NewGame()
    {
        Debug.Log("Generating new game data");
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load data using data handler
        gameData = dataHandler.Load();
        // if no data is laoded, initialize
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to default settings");
            NewGame();
        }
        this.dataPersistenceObjects = FindAllDataPersistenceObjects(); // refreshing just because?
        // Push the loaded dat to all other scripts with a loaddata function
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        Debug.Log("Save Game Called");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log("Number of persistant objects: " + this.dataPersistenceObjects.Count);
        if (dataPersistenceObjects != null) // logic handles if there is no persistent objects between scenes.
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(gameData);
            }
        }
        Debug.Log("Handing writing off to DataHandler");
        dataHandler.Save(gameData,OverwriteData);
    }


    #endregion


    #region Support Methods
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    #endregion
}