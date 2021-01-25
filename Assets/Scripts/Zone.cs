using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public event Action<Zone> ZoneDestroyed;

    [SerializeField]
    private bool isInvincible = false;
    
    [SerializeField]
    private Color fullHealthColor;
    
    [SerializeField]
    private Color halfHealthColor;
    
    [SerializeField]
    private Color lowHealthColor;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = fullHealthColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy == null)
        {
            var consumable = other.GetComponent<Consumable>();
            if (consumable == null)
            {
                return;
            }
            HandleConsumableCollision(consumable);
        }
        else
        {
            HandleEnemyCollision(enemy);
        }
    }

    private void HandleEnemyCollision(Enemy enemy)
    {
        enemy.OnEnemyDeath();

        if (!isInvincible && spriteRenderer.color == fullHealthColor)
        {
            spriteRenderer.color = halfHealthColor;
        }
        else if (!isInvincible && spriteRenderer.color == halfHealthColor)
        {
            spriteRenderer.color = lowHealthColor;
        }
        else if (!isInvincible)
        {
            OnZoneDestroyed();
        }
    }
    
    private void HandleConsumableCollision(Component consumable)
    {
        Destroy(consumable.gameObject);

        if (spriteRenderer.color == lowHealthColor)
        {
            spriteRenderer.color = halfHealthColor;
        }
        else if (spriteRenderer.color == halfHealthColor)
        {
            spriteRenderer.color = fullHealthColor;
        }
    }

    private void OnZoneDestroyed()
    {
        ZoneDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
