using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class ZombieController : MonoBehaviour
{
    List<Transform> validSpawnPoints = new List<Transform>();
    public GameObject[] spawnFolder;

    public GameObject zombiePrefab;
    private int zombiesToSpawn;
    public int waveCounter = 1;
    private int zombieCount;
    private int padding = 200;
    private Camera cam;

    System.Random rand = new System.Random();

    public void Start()
    {
        spawnFolder = new GameObject[transform.childCount];
        cam = Camera.main;

        for (int i = 0; i < transform.childCount; i++)
        {
            spawnFolder[i] = transform.GetChild(i).gameObject;
        }

        StartCoroutine(SpawnZombies());
    }

    public IEnumerator SpawnZombies()
    {
        while (true)
        {
            zombiesToSpawn = waveCounter * 7;
            zombieCount = zombiesToSpawn;
            
            for (int i = 0; i < zombiesToSpawn; i++)
            {
                yield return new WaitForSeconds(rand.Next(1, 6));

                RefreshSpawnList();

                int spawnPoint = rand.Next(1, validSpawnPoints.Count);
                GameObject zombie = Instantiate(zombiePrefab, validSpawnPoints[spawnPoint]);
                zombie.transform.parent = null;
            }

            yield return new WaitUntil(() => zombieCount == 0);
        }
    }

    private void RefreshSpawnList()
    {
        validSpawnPoints.Clear();

        foreach (GameObject spawnPoint in spawnFolder)
        {
            Vector2 spawnPosition = cam.WorldToScreenPoint(spawnPoint.transform.position);

            if (spawnPosition.x < 0 - padding|| spawnPosition.x > cam.pixelWidth + padding)
            {
                validSpawnPoints.Add(spawnPoint.transform);
                Debug.Log("ADDED VALID SPAWNPOINT AT: " + Convert.ToString(spawnPoint.transform.position));
            }
        }
    }
}