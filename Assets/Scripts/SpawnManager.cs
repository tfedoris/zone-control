using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float worldBoundaryX;
    private float worldBoundaryY;

    private void Start()
    {
        Vector3 worldBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        worldBoundaryX = worldBoundary.x + 0.5f;
        worldBoundaryY = worldBoundary.y + 0.5f;
    }

    private void Update()
    {
        
    }
}
