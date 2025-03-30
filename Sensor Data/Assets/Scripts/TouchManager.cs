using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    string _dataPath;
    string _jsonTouchData;
    private List<TouchRecord> touchData = new List<TouchRecord>();




    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions.FindAction("TouchPress");
        touchPositionAction = playerInput.actions.FindAction("TouchPosition");

        _dataPath = Application.persistentDataPath + "/Sensor_Data/";

        NewDirectory();
    }


    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
    }

    public void SaveButtonPressed()
    {
        SerializeJSON();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 screenPos = touchPositionAction.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = player.transform.position.z;
        player.transform.position = worldPos;

        TouchRecord record = new TouchRecord
        {
            time = Time.time,
            x = screenPos.x,
            y = screenPos.y
        };

        touchData.Add(record);
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

    public void SerializeJSON()
    {
        if (touchData != null && touchData.Count > 0)
        {
            TouchDataContainer container = new TouchDataContainer
            {
                touchRecords = touchData
            };

            string jsonString = JsonUtility.ToJson(container, true);

            string filePath = Path.Combine(_dataPath, "touch_data.json");
            File.WriteAllText(filePath, jsonString);

            Debug.Log($"Touch data serialized to JSON at: {filePath}");
        }
        else
        {
            Debug.Log("No data to serialize (JSON)");
        }
    }

    [Serializable]
    public class TouchRecord
    {
        public float time;
        public float x;
        public float y;
    }

    [Serializable]
    public class TouchDataContainer
    {
        public List<TouchRecord> touchRecords;
    }
}
