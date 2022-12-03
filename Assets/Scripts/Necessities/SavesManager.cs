using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class SavesManager : MonoBehaviour
{
    public static SavesManager instance;

    public SaveData activeSave;

    public bool hasLoaded;

    void Awake()
    {
        //Start - Code to test for copies
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Saves");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        //End - Code to test for copies

        instance = this;

        if (!hasLoaded)
        {
            activeSave.saveName = "Save Data";
        }

        Load();

        if (!hasLoaded)
        {
            Save();
            Debug.Log("Created new save");
        }
    }

    void Update()
    {
        
    }

    public void Save()
    {
        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Saved");
    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();

            Debug.Log("Loaded");

            hasLoaded = true;
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;


        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");

            Debug.Log("Deleted");
        }
    }



    //Whatever we need goes in here
    [System.Serializable]
    public class SaveData
    {
        public string saveName;

        //Put whatever we need here
        //Such as level completion, power-ups equipped, etc.
    }
}
