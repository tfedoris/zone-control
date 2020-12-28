using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputManager : MonoBehaviour
{
    private Interaction interaction;
    private Collider2D hit;
    
    // Update is called once per frame
    private void Update()
    {
        if (Touch.activeFingers.Count > 0)
        {
            Touch activeTouch = Touch.activeFingers[0].currentTouch;
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(activeTouch.screenPosition);
            if (activeTouch.phase == TouchPhase.Began)
            {
                hit = Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y));
                if (!hit)
                {
                    return;
                }
                interaction = hit.GetComponent<Interaction>();
            }
            if (interaction)
            {
                interaction.OnTouch(activeTouch, new Vector3(touchPosition.x, touchPosition.y, 0f));
            }
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
