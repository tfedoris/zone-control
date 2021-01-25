using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;
    
    [SerializeField]
    private float spawnTimeMin = 1.0f, spawnTimeMax = 3.0f;
    
    [SerializeField]
    private SpawnerPosition spawnerPosition = SpawnerPosition.Top;

    [SerializeField]
    private Vector2 offsetVector = new Vector2(1.5f, 0.5f);

    [SerializeField]
    private List<Zone> targetZones;

    [SerializeField]
    private Enemy[] enemyPrefabs;

    private float worldBoundaryX;
    private float worldBoundaryY;
    private List<Enemy> spawnedEnemies = new List<Enemy>();
    private float timeSinceLastSpawn;
    private float currentSpawnDelay;

    private enum SpawnerPosition
    {
        TopLeft,
        TopRight,
        Top,
        BottomLeft,
        BottomRight,
        Bottom
    }

    private void OnEnable()
    {
        SubscribeTargetZones();
    }

    private void OnDisable()
    {
        UnsubscribeTargetZones();
    }

    private void Start()
    {
        Vector2 offset;
        Vector2 screenPoint;

        if (Camera.main is null)
        {
            return;
        }

        switch (spawnerPosition)
        {
            case SpawnerPosition.TopLeft:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height);
                offset = new Vector2(-offsetVector.x, offsetVector.y);
                break;
            case SpawnerPosition.TopRight:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height);
                offset = new Vector2(offsetVector.x, offsetVector.y);
                break;
            case SpawnerPosition.Top:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height);
                offset = new Vector2(0f, offsetVector.y);
                break;
            case SpawnerPosition.BottomLeft:
                screenPoint = new Vector2(Screen.width / 2f, 0f);
                offset = new Vector2(-offsetVector.x, -offsetVector.y);
                break;
            case SpawnerPosition.BottomRight:
                screenPoint = new Vector2(Screen.width / 2f, 0f);
                offset = new Vector2(offsetVector.x, -offsetVector.y);
                break;
            case SpawnerPosition.Bottom:
                screenPoint = new Vector2(Screen.width / 2f, 0f);
                offset = new Vector2(0f, -offsetVector.y);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 0));
        worldPoint = new Vector3(worldPoint.x + offset.x, worldPoint.y + offset.y, 0f);
        transform.position = worldPoint;
        currentSpawnDelay = Random.Range(spawnTimeMin, spawnTimeMax);
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnDelay && enemyPrefabs.Length > 0 && targetZones.Count > 0)
        {
            int enemyIndex = 0;
            enemyIndex = Random.Range(0, enemyPrefabs.Length);
            SpawnEnemy(enemyIndex);
            timeSinceLastSpawn = 0;
            currentSpawnDelay = Random.Range(spawnTimeMin, spawnTimeMax);
        }
    }

    private void SpawnEnemy(int index)
    {
        int targetIndex = Random.Range(0, targetZones.Count);
        if (!targetZones[targetIndex] || !enemyPrefabs[index])
        {
            return;
        }
        enemyPrefabs[index].target = targetZones[targetIndex].transform;
        spawnedEnemies.Add(Instantiate(enemyPrefabs[index], transform.position, transform.rotation));
        spawnedEnemies.Last().EnemyDeath += HandleEnemyDeath;
        spawnedEnemies.Last().EnemyKilledByPlayer += scoreManager.HandleEnemyKilledByPlayer;
    }

    private void SubscribeTargetZones()
    {
        foreach (var targetZone in targetZones)
        {
            targetZone.ZoneDestroyed += HandleZoneDestroyed;
        }
    }
    
    private void UnsubscribeTargetZones()
    {
        foreach (var targetZone in targetZones)
        {
            targetZone.ZoneDestroyed -= HandleZoneDestroyed;
        }
        targetZones.Clear();
    }
    
    private void HandleEnemyDeath(Enemy enemy)
    {
        spawnedEnemies.RemoveAll(x => x == enemy);
        enemy.EnemyDeath -= HandleEnemyDeath;
        enemy.EnemyKilledByPlayer -= scoreManager.HandleEnemyKilledByPlayer;
    }

    private void HandleZoneDestroyed(Zone zone)
    {
        targetZones.RemoveAll(x => x == zone);
        foreach (var spawnedEnemy in spawnedEnemies.Where(spawnedEnemy => spawnedEnemy.target == zone.transform))
        {
            FindNewTarget(spawnedEnemy);
        }
    }

    private void FindNewTarget(Enemy enemy)
    {
        if (targetZones.Count > 0)
        {
            int targetIndex = Random.Range(0, targetZones.Count);
            enemy.target = targetZones[targetIndex].transform;
        }
        else
        {
            Destroy(enemy.gameObject);
        }
    }

    private void DestroyAllEnemies()
    {
        foreach (var spawnedEnemy in spawnedEnemies)
        {
            Destroy(spawnedEnemy.gameObject);
        }
        spawnedEnemies.Clear();
    }
}
