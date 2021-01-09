using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AnchorToPointOnScreen : MonoBehaviour
{
    [SerializeField]
    private AnchorPoint anchorPoint = AnchorPoint.Center;
    
    private float spriteSize;
    private Vector2 screenPoint;
    private Camera mainCamera;
    private Rect safeArea;

    private enum AnchorPoint
    {
        Top,
        Center,
        Bottom
    }

    private void Start()
    {
        mainCamera = Camera.main;
        safeArea = Screen.safeArea;
        
        if (mainCamera is null)
        {
            return;
        }

        switch (anchorPoint)
        {
            case AnchorPoint.Top:
                screenPoint = new Vector2(Screen.safeArea.width / 2f, Screen.safeArea.height);
                break;
            case AnchorPoint.Center:
                screenPoint = new Vector2(Screen.safeArea.width / 2f, Screen.safeArea.height / 2f);
                break;
            case AnchorPoint.Bottom:
                screenPoint = new Vector2(Screen.safeArea.width / 2f, 0f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 0f));

        transform.position = new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
}
