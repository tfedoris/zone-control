using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputManager : Singleton<InputManager>
{
    private Interactable interactable;
    private Collider2D hit;
    private Camera cameraMain;

    private void Start()
    {
        cameraMain = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Touch.activeFingers.Count <= 0)
        {
            return;
        }
        
        Touch activeTouch = Touch.activeFingers[0].currentTouch;
        Vector3 touchPosition = cameraMain.ScreenToWorldPoint(activeTouch.screenPosition);
        if (activeTouch.phase == TouchPhase.Began)
        {
            hit = Physics2D.OverlapPoint(new Vector2(touchPosition.x, touchPosition.y));
            if (!hit)
            {
                return;
            }
            interactable = hit.GetComponent<Interactable>();
        }
        if (interactable)
        {
            interactable.OnTouch(activeTouch, new Vector3(touchPosition.x, touchPosition.y, 0f));
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
