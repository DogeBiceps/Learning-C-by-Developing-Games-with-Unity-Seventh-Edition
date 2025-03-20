using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    private string _dataPath;
    private string _xmlGroupMembers;
    private string _jsonGroupMembers;
    private List<Member> groupMembers = new List<Member>
    {
        new Member("Mathilde", "27/9/2003", "orange"),
        new Member("Rasmine", "26/1/2000", "blue"),
        new Member("August", "21/3/2003", "blue"),
        new Member("Oskar", "10/7/2000", "green"),
        new Member("Oscar", "6/9/2000", "yellow"),
        new Member("Tomas", "25/1/2001", "purple"),

    };
    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Group_Data/";
        _xmlGroupMembers = _dataPath + "GroupMembers.xml";
        _jsonGroupMembers = _dataPath + "MembersJSON.json";
        Initialize();
    }

    public void Initialize()
    {
        NewDirectory();
        // Could also use WriteToXML, but SerializeXML works fine for this task and is more efficient.
        SerializeXML(groupMembers);
        DeserializeXML();
    }

    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            return;
        }
        Directory.CreateDirectory(_dataPath);
        Debug.Log("New directory created");
    }

    // Serializes a given list of members into XML
    public void SerializeXML(List<Member> groupMembers)
    {
        // Ensure data exists and is not empty
        if (groupMembers != null && groupMembers.Count > 0)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Member>));
            using (FileStream stream = File.Create(_xmlGroupMembers))
            {
                xmlSerializer.Serialize(stream, groupMembers);
            }

        }
        else
        {
            Debug.Log("No data to serialize (XML)");
            return;
        }
    }


    public void DeserializeXML()
    {
        if (File.Exists(_xmlGroupMembers))
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Member>));
            using (FileStream stream = File.OpenRead(_xmlGroupMembers))
            {
                // Save deserialized list as new var
                var deserializedMembers = (List<Member>)xmlSerializer.Deserialize(stream);

                // Verify correct xml deserialization by displaying member attributes.
                foreach (var member in deserializedMembers)
                {
                    Debug.LogFormat("Name: {0} - Date of birth: {1} - Favorite color: {2} - XML works", member.name, member.dateOfBirth, member.favColor);
                }
                    // Take the new var and serialize it to .json
                    SerializeJSON(deserializedMembers);
            }
        }
    }


    //Serializes a given list of members to JSON
    public void SerializeJSON(List<Member> membersToSerialize)
    {
        // Ensure data exists and is not empty before serializing
        if (membersToSerialize != null && membersToSerialize.Count > 0)
        {
            GroupMembers members = new GroupMembers();
            members.groupMembers = membersToSerialize;
            string jsonString = JsonUtility.ToJson(members, true);
            using (StreamWriter stream = File.CreateText(_jsonGroupMembers))
            {
                stream.WriteLine(jsonString);
            }
        }
        else
        {
            Debug.Log("No data to serialize (JSON)");
            return;
        }
    }
}
