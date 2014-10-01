using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PointBlock : BaseBlock
{
    public int HitPoints = 1;
    bool wasDamaged = false;

    void Start()
    {
        base.Start();

        AdjustColor();
    }

    void Update()
    {
        // it's a new frame! we can take damage again
        wasDamaged = false;
    }

    public void AdjustColor()
    {
        Color newColor = Color.white;

        switch (HitPoints)
        {
            case 1:
                newColor = Color.blue;
                break;
            case 2:
                newColor = Color.green;
                break;
            case 3:
                newColor = Color.yellow;
                break;
            case 4:
                newColor = new Color(1f,0.4f,0f); // orange
                break;
            case 5:
                newColor = Color.red;
                break;
        }
        sprite.color = newColor;
    }
    
    // we've been hit!
    protected override void Damage()
    {
        // avoid triggering damage twice in the same frame
        if (wasDamaged) return;
        wasDamaged = true;

        --HitPoints;
        if (HitPoints <= 0)
        {
            Die();
            return;
        }
        AdjustColor();
    }

    void Die()
    {
        sprite.enabled = false;
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D coll in colliders)
        {
            coll.enabled = false;
        }
        isDead = true;
    }
}