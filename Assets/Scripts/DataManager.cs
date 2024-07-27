using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// depreicated
    /// 
    /// </summary>
    public static DataManager instance;

    [Header("File Settings")]
    public string save_file_name = "GameData";
    public string folder_name = "SaveData";


    [Header("Data Settings")]
    public DefaultData game_data = new DefaultData();
    public LocationDirectory location_directory_object = new LocationDirectory();

    string default_path;
    string filename;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        default_path = Application.persistentDataPath + "/" + folder_name;
        filename = default_path + "/" + save_file_name + ".json";
        if (!FolderExists(default_path))
        {
            Directory.CreateDirectory(default_path);
        }

        //LoadGameData();
        SaveGameData();
    }
    bool FolderExists(string path)
    {
        return Directory.Exists(path);
    }

    public void LoadGameData()
    {
        if (File.Exists(filename))
        {
            string save_data = File.ReadAllText(filename);
            game_data = JsonUtility.FromJson<DefaultData>(save_data);
        }
    }

    public void SaveGameData()
    {
        string save_data = JsonUtility.ToJson(location_directory_object);
        File.WriteAllText(filename, save_data);
    }

}
