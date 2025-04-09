using UnityEngine;
using System.Collections.Generic;

public class Accelerometer : MonoBehaviour
{
    float speed = 10.0f;
    public List<AccelRecord> AccelData = new List<AccelRecord>();
    float sampleInterval = 0.1f; 
    float lastSampleTime = 0.0f;


    void Update()
    {
        if (Time.time - lastSampleTime >= sampleInterval)
        {
            GetAccelerometerValue();
            lastSampleTime = Time.time;
        }
        Vector3 dir = Vector3.zero;
        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis

        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);
    }


    Vector3 GetAccelerometerValue()
    {
        Vector3 acc = Vector3.zero;
        float period = 0.0f;

        foreach (AccelerationEvent evnt in Input.accelerationEvents)
        {
            acc += evnt.acceleration * evnt.deltaTime;
            period += evnt.deltaTime;
        }
        if (period > 0)
        {
            acc *= 1.0f / period;
        }

 
        AccelRecord record = new AccelRecord
        {
            time = Time.time,
            accel = acc
        };
        AccelData.Add(record);
        return acc;
    }

}

[System.Serializable]
public class AccelRecord
{
    public float time;
    public Vector3 accel;
}

[System.Serializable]
public class AccelDataContainer
{
    public List<AccelRecord> accelRecords;
}