using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private SpawnerPosition spawnerPosition = SpawnerPosition.Top;

    [SerializeField]
    private Vector2 offsetVector = new Vector2(0.5f, 0.5f);

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Enemy[] enemyPrefabs;

    private float worldBoundaryX;
    private float worldBoundaryY;
    private Rect safeArea;

    private enum SpawnerPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Left,
        Right,
        Top,
        Bottom
    }
    
    private void Start()
    {
        Vector2 offset;
        Vector2 screenPoint;
        safeArea = Screen.safeArea;
        
        if (Camera.main is null)
        {
            return;
        }

        switch (spawnerPosition)
        {
            case SpawnerPosition.TopLeft:
                screenPoint = new Vector2(0f, safeArea.height);
                offset = new Vector2(-offsetVector.x, offsetVector.y);
                break;
            case SpawnerPosition.TopRight:
                screenPoint = new Vector2(safeArea.width, safeArea.height);
                offset = new Vector2(offsetVector.x, offsetVector.y);
                break;
            case SpawnerPosition.BottomLeft:
                screenPoint = new Vector2(0f, 0f);
                offset = new Vector2(-offsetVector.x, -offsetVector.y);
                break;
            case SpawnerPosition.BottomRight:
                screenPoint = new Vector2(safeArea.width, 0f);
                offset = new Vector2(offsetVector.x, -offsetVector.y);
                break;
            case SpawnerPosition.Left:
                screenPoint = new Vector2(0f, safeArea.height / 2f);
                offset = new Vector2(-offsetVector.magnitude, 0f);
                break;
            case SpawnerPosition.Right:
                screenPoint = new Vector2(safeArea.width, safeArea.height / 2f);
                offset = new Vector2(offsetVector.magnitude, 0f);
                break;
            case SpawnerPosition.Top:
                screenPoint = new Vector2(safeArea.width / 2f, safeArea.height);
                offset = new Vector2(0f, offsetVector.magnitude);
                break;
            case SpawnerPosition.Bottom:
                screenPoint = new Vector2(safeArea.width / 2f, 0f);
                offset = new Vector2(0f, -offsetVector.magnitude);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 0));
        worldPoint = new Vector3(worldPoint.x + offset.x, worldPoint.y + offset.y, 0f);
        transform.position = worldPoint;

        if (enemyPrefabs.Length > 0)
        {
            SpawnEnemy(0);
        }
    }

    private void Update()
    {
        
    }

    private void SpawnEnemy(int index)
    {
        enemyPrefabs[index].target = target;
        Instantiate(enemyPrefabs[index], transform.position, transform.rotation);
    }
}
