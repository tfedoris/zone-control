using System;
using System.Collections;
using System.Collections.Generic;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using UnityEngine;

public class Enemy : Interactable
{
    public event Action<Enemy> EnemyDeath;
    public event Action EnemyKilledByPlayer;

    public Transform target;

    [SerializeField]
    private Consumable consumablePrefab;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private double tapWindow = 0.1;

    private void Update()
    {
        RotateTowardsTarget();
        MoveTowardsTarget();
    }

    public override void OnTouch(Touch touch, Vector3 touchPosition)
    {
        // Destroy on touch
        if (touch.phase != TouchPhase.Began && (touch.time - touch.startTime) > tapWindow)
        {
            return;
        }

        OnEnemyKilledByPlayer();
    }
    
    public void OnEnemyDeath()
    {
        EnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }
    
    public void OnEnemyKilledByPlayer()
    {
        EnemyKilledByPlayer?.Invoke();
        OnEnemyDeath();
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
