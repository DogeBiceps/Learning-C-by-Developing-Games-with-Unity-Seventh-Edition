using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class DataManager : MonoBehaviour, IManager
{
    private string _dataPath;
    private string _state;
    private string _textFile;
    private string _streamingTextFile;
    private string _xmlLevelProgress;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    private string _xmlWeapons;
    private string _jsonWeapons;
    private List<Weapon> weaponInventory = new List<Weapon>
    {
        new Weapon("Sword of Doom", 100),
        new Weapon("Butterfly knives", 25),
        new Weapon("Brass Knuckles", 15),
    };



    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        Debug.Log(_dataPath);
        _textFile = _dataPath + "Save_Data.txt";
        _streamingTextFile = _dataPath + "Streaming_Save_Data.txt";
        _xmlLevelProgress = _dataPath + "Progress_Data.xml";
        _xmlWeapons = _dataPath + "WeaponInventory.xml";
        _jsonWeapons = _dataPath + "WeaponsJSON.json";
        Initialize();
    }

    public void Initialize()
    {
        _state = "Data Manager initialized..";
        Debug.Log(_state);
        WriteToStream(_streamingTextFile);
        ReadFromStream(_streamingTextFile);
        WriteToXML(_xmlLevelProgress);
        ReadFromStream(_xmlLevelProgress);
        SerializeXML();
        DeserializeXML();
        SerializeJSON();
        DeserializeJSON();

        /*
        NewDirectory();
        NewTextFile();
        UpdateTextFile();
        // DeleteDirectory();
        */
    }

    public void FilesystemInfo()
    {
        Debug.LogFormat("Path separator character: {0}",
        Path.PathSeparator);
        Debug.LogFormat("Directory separator character: {0}",
        Path.DirectorySeparatorChar);
        Debug.LogFormat("Current directory: {0}",
        Directory.GetCurrentDirectory());
        Debug.LogFormat("Temporary path: {0}",
        Path.GetTempPath());
    }

    public void NewDirectory()
    {
        Debug.Log("NewDirectory called");
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }
        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory created!");
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted");

            return;
        }

        Directory.Delete(_dataPath, true);
        Debug.Log("Directory successfully deleted");
    }

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists");
            return;
        }

        File.WriteAllText(_textFile, "<SAVE DATA>\n\n");

        Debug.Log("New file created");
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
                Debug.Log("File doesn't exist...");
                return;
            }
        File.AppendAllText(_textFile, $"\n Game_started: {DateTime.Now} \n");
        // File.WriteAllText(_textFile, $"Game_started: {DateTime.Now}");
        Debug.Log("File updated");
    }

    public void DeleteFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesnt exist or has already been deleted");
            return;
        }

        File.Delete(_textFile);
        Debug.Log("File deleted");
    }

    public void WriteToStream(string filename)
    {
        if (!File.Exists(filename))
        {
            StreamWriter newStream = File.CreateText(filename);

            newStream.WriteLine("<Save Data> for HERO FORN \n \n");
            newStream.Close();
            Debug.Log("New file created with StreamWriter");
        }
        StreamWriter streamWriter = File.AppendText(filename);
        streamWriter.WriteLine("Game Ended:" + DateTime.Now);
        streamWriter.Close();
        Debug.Log("File Concents updated with streamwriter");
    }

    public void ReadFromStream(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesnt exist");
            return;
        }
        StreamReader streamReader = new StreamReader(filename);
        Debug.Log(streamReader.ReadToEnd());
    }
    
    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename))
        {
            FileStream xmlStream = File.Create(filename);
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("level_progress");

            for (int i = 1; i < 5; i++)
            {
                xmlWriter.WriteElementString("level", "Level-" + i);
            }

            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            xmlStream.Close();
        }
    }

    public void SerializeXML()
    {
        var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));
        using(FileStream stream = File.Create(_xmlWeapons))
        {
            xmlSerializer.Serialize(stream, weaponInventory);
        }
    }

    public void DeserializeXML()
    {
        if (File.Exists(_xmlWeapons))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Weapon>));
            using (FileStream stream = File.OpenRead(_xmlWeapons))
            {
                var weapons = (List<Weapon>)xmlSerializer.Deserialize(stream);

                foreach(var weapon in weapons)
                {
                    Debug.LogFormat("Weapon: {0} - Damage: {1}", weapon.name, weapon.damage);
                }
            }
        }
    }

    public void SerializeJSON()
    {
        WeaponShop shop = new WeaponShop();
        shop.inventory = weaponInventory;
        string jsonString = JsonUtility.ToJson(shop, true);
        using(StreamWriter stream = File.CreateText(_jsonWeapons))
        {
            stream.WriteLine(jsonString);
        }
    }

    public void DeserializeJSON()
    {
        if (File.Exists(_jsonWeapons))
        {
            using (StreamReader stream = new StreamReader(_jsonWeapons))
            {
                var jsonString = stream.ReadToEnd();
                var weaponData = JsonUtility.FromJson<WeaponShop>(jsonString);

                foreach(var weapon in weaponData.inventory)
                {
                    Debug.LogFormat("Weapon:{0} - Damage: {1}", weapon.name, weapon.damage);
                }
            }
        }
    }
}
