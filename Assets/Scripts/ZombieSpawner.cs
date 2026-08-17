using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner instance;

    public GameObject[] zombiePrefabs;
    public Transform[] spawnPoints;
    public float delayBetweenSpawns = 0.5f;

    void Awake()
    {
        instance = this;
    }

    public void SpawnWave(int zombieCount)
    {
        StartCoroutine(SpawnZombiesRoutine(zombieCount));
    }

    IEnumerator SpawnZombiesRoutine(int zombieCount)
    {
        for (int i = 0; i < zombieCount; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    void SpawnZombie()
    {
        int randomPrefabIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject chosenPrefab = zombiePrefabs[randomPrefabIndex];

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        Instantiate(chosenPrefab, chosenPoint.position, Quaternion.identity);
    }
}