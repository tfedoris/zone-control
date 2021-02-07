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
    private Transform leftZoneTransform, centerZoneTransform, rightZoneTransform;

    private Camera mainCamera;
    private float timeSinceLastSpawn;
    private float spawnXMin;
    private float spawnXMax;
    private float centerZoneY;
    private float belowLowerQuartileY;
    private float belowUpperQuartileY;
    private float aboveLowerQuartileY;
    private float aboveUpperQuartileY;
    private bool isCenter = false;

    private void Start()
    {
        mainCamera = Camera.main;
        if (!mainCamera)
        {
            return;
        }

        spawnXMin = leftZoneTransform.position.x;
        spawnXMax = rightZoneTransform.position.x;
        
        centerZoneY = centerZoneTransform.position.y;

        isCenter = (centerZoneY == 0f);
        
        Vector2 screenPositionMin = new Vector2(Screen.safeArea.xMin, Screen.safeArea.yMin);
        Vector2 screenPositionMax = new Vector2(Screen.safeArea.xMax, Screen.safeArea.yMax);
        
        float worldYMin = mainCamera.ScreenToWorldPoint(screenPositionMin).y;
        float worldYMax = mainCamera.ScreenToWorldPoint(screenPositionMax).y;

        belowLowerQuartileY = (worldYMin + centerZoneY) * 0.25f;
        belowUpperQuartileY = (worldYMin + centerZoneY) * 0.75f;
        
        aboveLowerQuartileY = (centerZoneY + worldYMax) * 0.25f;
        aboveUpperQuartileY = (centerZoneY + worldYMax) * 0.75f;
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
                spawnPositionX = Random.Range(spawnXMin, spawnXMax);
                spawnPositionY = Random.Range(aboveLowerQuartileY, aboveUpperQuartileY);
            }
            else
            {
                // Spawn below zones
                spawnPositionX = Random.Range(spawnXMin, spawnXMax);
                spawnPositionY = Random.Range(belowLowerQuartileY, belowUpperQuartileY);
            }

            Vector3 spawnPosition = new Vector3(spawnPositionX, spawnPositionY, 0f);
            Instantiate(consumablePrefab, spawnPosition, Quaternion.identity);
            timeSinceLastSpawn = 0f;
        }
    }
}
