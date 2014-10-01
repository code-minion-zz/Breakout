using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    public enum EDirection
    {
        LEFT,
        RIGHT
    }
    public static Paddle Instance;
    SpriteRenderer sprite;
    Color paddleColor;
    const float PADDLEBOUNDARY = 7.75f;
    const float PADDLESPEED = 0.1f;

	// Use this for initialization
	void Awake () 
    {
	}

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        paddleColor = sprite.color;
    }

    public void Move(EDirection direction)
    {
        Vector2 pos = transform.position;

        switch (direction)
        {
            case EDirection.LEFT:
                pos.x -= PADDLESPEED;
                if (pos.x < -PADDLEBOUNDARY)
                {
                    pos.x = -PADDLEBOUNDARY;
                }
                break;
            case EDirection.RIGHT:
                pos.x += PADDLESPEED;
                if (pos.x > PADDLEBOUNDARY)
                {
                    pos.x = PADDLEBOUNDARY;
                }
                break;
        }

        transform.position = pos;
    }

    // Flash white, then change color back to original after a short delay.
    public void Flash() 
    {
        sprite.color = Color.white;
        StartCoroutine(RevertColor(0.5f));
    }

    IEnumerator RevertColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.color = paddleColor;
    }
}
