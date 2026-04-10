using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject collectible;
    [SerializeField] private GameObject[] obstacles;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;
    [SerializeField] private float spawnX = 10f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        if (collectible == null || obstacles.Length == 0)
        {
            Debug.LogError("Spawner prefabs not assigned!");
            return;
        }

        float yPos = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(spawnX, yPos);
        GameObject objToSpawn;
        if (Random.value > 0.8f)
        {
            objToSpawn = collectible;
        }
        else
        {
            int index = Random.Range(0, obstacles.Length);
            objToSpawn = obstacles[index];
        }
        Instantiate(objToSpawn, spawnPos, Quaternion.identity);
    }
}
