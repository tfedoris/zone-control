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
    private float offset = 0f;

    private enum AnchorPoint
    {
        Top,
        CenterLeft,
        Center,
        CenterRight,
        Bottom,
        BottomLeft,
        BottomRight
    }

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera is null)
        {
            return;
        }

        switch (anchorPoint)
        {
            case AnchorPoint.Top:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height);
                break;
            case AnchorPoint.Center:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                break;
            case AnchorPoint.Bottom:
                screenPoint = new Vector2(Screen.width / 2f, 300f);
                break;
            case AnchorPoint.CenterLeft:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                offset = -1.5f;
                break;
            case AnchorPoint.CenterRight:
                screenPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                offset = 1.5f;
                break;
            case AnchorPoint.BottomLeft:
                screenPoint = new Vector2(Screen.width / 2f, 300f);
                offset = -1.5f;
                break;
            case AnchorPoint.BottomRight:
                screenPoint = new Vector2(Screen.width / 2f, 300f);
                offset = 1.5f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, 0f));

        transform.position = new Vector3(worldPoint.x + offset, worldPoint.y, 0f);
    }
}
