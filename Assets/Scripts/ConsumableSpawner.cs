using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConsumableSpawner : MonoBehaviour
{
    [SerializeField]
    private Consumable consumablePrefab;

    [SerializeField]
    private float spawnDelay = 30.0f;

    private Camera mainCamera;
    private float timeSinceLastSpawn;
    private Vector2 spawnPositionMin;
    private Vector2 spawnPositionMax;

    private void Start()
    {
        mainCamera = Camera.main;
        if (!mainCamera)
        {
            return;
        }
        Vector2 screenPositionMin = new Vector2(150f, 150f);
        Vector2 screenPositionMax = new Vector2(Screen.width - 150f, Screen.height - 150f);
        spawnPositionMin = mainCamera.ScreenToWorldPoint(screenPositionMin);
        spawnPositionMax = mainCamera.ScreenToWorldPoint(screenPositionMax);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnDelay)
        {
            float spawnPositionX;
            float spawnPositionY;
            
            if (Random.value > 0.5f)
            {
                // Spawn above zones
                spawnPositionX = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
                spawnPositionY = Random.Range(spawnPositionMin.y, -2.5f);
            }
            else
            {
                // Spawn below zones
                spawnPositionX = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
                spawnPositionY = Random.Range(2.5f, spawnPositionMax.y);
            }

            Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0f);
            Instantiate(consumablePrefab, spawnPosition, Quaternion.identity);
            timeSinceLastSpawn = 0f;
        }
    }
}
