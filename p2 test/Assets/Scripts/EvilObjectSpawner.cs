using UnityEngine;

public class SidewaysMoverSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 5f;

    public Transform spawnPoint;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
