using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour, IManager
{
    private string _dataPath;
    private string _state;
    private string _textFile;
    private int i;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";
        _textFile = _dataPath + "Save_Data.txt";
        Debug.Log(_dataPath);
        Initialize();
    }

    public void Initialize()
    {
        _state = "Data Manager initialized..";
        Debug.Log(_state);
        NewDirectory();
        NewTextFile();
        UpdateTextFile();
        // DeleteDirectory();
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
            // 2
            Debug.Log("Directory already exists...");
            return;
        }
        // 3
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
        if (!File.Exists(_textFile) && i < 1)
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        File.AppendAllText(_textFile, $"\n Game_started: {DateTime.Now} \n");
        // File.WriteAllText(_textFile, $"Game_started: {DateTime.Now}");
        Debug.Log("File updated");
        i++;
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
}
