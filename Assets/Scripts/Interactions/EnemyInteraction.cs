using System.Collections;
using System.Collections.Generic;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine;

public class EnemyInteraction : Interaction
{
    public override void OnTouch(Touch touch, Vector3 touchPosition)
    {
        if (!touch.isInProgress)
        {
            return;
        }
        
        Destroy(gameObject);
    }
}
