using System;
using System.Collections;
using System.Collections.Generic;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine;

public class Enemy : Interactable
{
    public Transform target;
    
    [SerializeField]
    private float speed = 5f;

    private void Update()
    {
        RotateTowardsTarget();
        MoveTowardsTarget();
    }

    public override void OnTouch(Touch touch, Vector3 touchPosition)
    {
        // Destroy on touch
        if (!touch.isInProgress)
        {
            return;
        }
        
        Destroy(gameObject);
    }

    protected virtual void MoveTowardsTarget()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
    
    private void RotateTowardsTarget()
    {
        Vector3 relativePosition = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, relativePosition);
        transform.rotation = rotation;
    }
}
