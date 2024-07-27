using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";    // desired path
    private string dataFileName = "";   // desired filename
    private string outputFileName = ""; // modified filename for overwrite protection
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "overcomplicated";
    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string full_path = Path.Combine(dataDirPath, dataFileName);
        GameData LoadedData = null;
        if (File.Exists(full_path))
        {
            try
            {
                string data_to_load;
                using (FileStream stream = new FileStream(full_path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        data_to_load = reader.ReadToEnd();
                    }
                }
                // data needs deserialized

                if (useEncryption)
                {
                    data_to_load = EncryptDecrypt(data_to_load);
                }


                LoadedData = JsonUtility.FromJson<GameData>(data_to_load);
            }
            catch (Exception e)
            {

                Debug.LogError("Error" + e);
            }
        }
        return LoadedData;
    }

    public void Save(GameData data, bool overwrite = false)
    {
        string full_path = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(full_path));
            // serialize C# game data object into json
            string dataToStore = JsonUtility.ToJson(data,true);
            if (overwrite && File.Exists(full_path))
            {
                Debug.Log("previous save data found, creating unique safe file name");
                int itnum = 0;
                string path_ext = Path.GetExtension(dataFileName); // get extension from desired name
                string path_name = Path.GetFileNameWithoutExtension(dataFileName); // get handle from desired name
                while (File.Exists(full_path))
                {
                    itnum = itnum + 1;
                    full_path = Path.Combine(dataDirPath, path_name + itnum + path_ext);
                }
                outputFileName = path_name + itnum + path_ext;
                Debug.Log("Saving file as: " + outputFileName);
            }
            else
            {
                outputFileName = dataFileName;
            }

            full_path = Path.Combine(dataDirPath, outputFileName);


            if (useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(full_path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("FileDataHandler.Save(): " + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);

        }

        return modifiedData;

    }

}
