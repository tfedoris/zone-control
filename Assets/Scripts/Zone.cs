using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public event Action<Zone> ZoneDestroyed;
    public event Action<int> ConsumableScored;
    
    [SerializeField]
    private ScoreManager scoreManager;

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

    private void OnEnable()
    {
        ConsumableScored += scoreManager.HandleScoreIncrease;
    }
    
    private void OnDisable()
    {
        ConsumableScored -= scoreManager.HandleScoreIncrease;
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
    
    private void HandleConsumableCollision(Consumable consumable)
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
        else
        {
            ConsumableScored?.Invoke(consumable.PointWorth);
        }
    }

    private void OnZoneDestroyed()
    {
        ZoneDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
