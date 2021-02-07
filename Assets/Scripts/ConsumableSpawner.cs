using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ConsumableSpawner : MonoBehaviour
{
    [SerializeField]
    private Consumable consumablePrefab;

    [SerializeField]
    private float spawnDelay = 30.0f;

    [SerializeField]
    private Transform zoneTransform;

    private Camera mainCamera;
    private float timeSinceLastSpawn;
    private Vector2 spawnPositionMin;
    private Vector2 spawnPositionMax;
    private bool isCenter = false;
    private float zonePositionY;

    private void Start()
    {
        mainCamera = Camera.main;
        if (!mainCamera)
        {
            return;
        }
        Vector2 screenPositionMin = new Vector2(Screen.safeArea.xMin + 300f, Screen.safeArea.yMin + 300f);
        Vector2 screenPositionMax = new Vector2(Screen.safeArea.xMax - 300f, Screen.safeArea.yMax - 300f);
        spawnPositionMin = mainCamera.ScreenToWorldPoint(screenPositionMin);
        spawnPositionMax = mainCamera.ScreenToWorldPoint(screenPositionMax);

        zonePositionY = zoneTransform.position.y;
        isCenter = (zonePositionY == 0f);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnDelay)
        {
            float spawnPositionX;
            float spawnPositionY;
            
            if (!isCenter || Random.value > 0.5f)
            {
                // Spawn above zones
                spawnPositionX = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
                spawnPositionY = Random.Range(zonePositionY + 5f, spawnPositionMax.y);
            }
            else
            {
                // Spawn below zones
                spawnPositionX = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
                spawnPositionY = Random.Range(spawnPositionMin.y, zonePositionY - 5f);
            }

            Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0f);
            Instantiate(consumablePrefab, spawnPosition, Quaternion.identity);
            timeSinceLastSpawn = 0f;
        }
    }
}
