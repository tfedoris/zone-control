using System.Collections;
using System.Collections.Generic;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine;

public class Consumable : Interactable
{
    [SerializeField]
    private int pointWorth = 10;

    public int PointWorth
    {
        get => pointWorth;

        private set => pointWorth = value;
    }
    
    public override void OnTouch(Touch touch, Vector3 touchPosition)
    {
        // Drag on Touch
        if (!touch.isInProgress)
        {
            return;
        }
        
        transform.position = touchPosition;
    }
}
