using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    SpriteRenderer sprite;
    Color paddleColor;
    public bool IsResting = true;
    Transform ballAnchor;

	// Use this for initialization
    void Awake() 
    {
        ballAnchor = GameObject.Find("BallAnchor").transform;
	}

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        paddleColor = sprite.color;
        Respawn();
    }

    // zero out velocity, move ball back to paddle
    public void Respawn()
    {
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.zero;
        transform.parent = ballAnchor;
        transform.localPosition = Vector3.zero;
        IsResting = true;
        Game.Instance.PlaySound(Game.SoundClip.GameOver);
    }

    // de-anchor ball from paddle, launch in random direction
    public void Launch()
    {
        rigidbody2D.isKinematic = false;
        float xVelocity = 0;
        IsResting = false;
        float parentVelocityX = Game.Instance.direction;
        transform.parent = null;

        //pick a direction and go for it
        xVelocity = Random.Range(-1f, 1f);
        // also take paddle movement into account when launching
        xVelocity += parentVelocityX;
        rigidbody2D.velocity = new Vector2(xVelocity, 2f);
        collider2D.enabled = true;
    }

    // Unity physics isn't suitable for this game, we'll make everything a trigger and handle physics ourselves!
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 velocity = rigidbody2D.velocity;

        if (other.gameObject.name == "Paddle")
        {
            // flash the Paddle to show that contact was made
            other.gameObject.GetComponent<Paddle>().Flash();

            velocity.y = 2f;
            // also take paddle movement into account when bouncing off it
            velocity.x += Game.Instance.direction;

            Game.Instance.PlaySound(Game.SoundClip.Paddle);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            if (other.gameObject.name == "Top")
            {
                velocity.y = 2f;
            }
            else if (other.gameObject.name == "Left")
            {
                velocity.x = -2f;
            }
            else if (other.gameObject.name == "Right")
            {
                velocity.x = 2f;
            }
            else
            {
                velocity.y = -2f;
            }

            Game.Instance.PlaySound(Game.SoundClip.Block);
            other.transform.parent.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (other.gameObject.name == "North")
            {
                velocity.y = -2f;
            }
            else if (other.gameObject.name == "West")
            {
                velocity.x = 2f;
            }
            else if (other.gameObject.name == "East")
            {
                velocity.x = -2f;
            }
            else // We've hit the bottom of the screen, we're dead!
            {
                Respawn();
                return;
            }
            Game.Instance.PlaySound(Game.SoundClip.Block);
        }
        rigidbody2D.velocity = velocity;
    }

    public void Flash() 
    {
        sprite.color = Color.blue;
        StartCoroutine(RevertColor(0.5f));
    }

    IEnumerator RevertColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        sprite.color = paddleColor;
    }
}
