using UnityEngine;
using System.Collections;

public class BaseBlock : MonoBehaviour {

    protected SpriteRenderer sprite;
    protected bool isDead = false;

	// Use this for initialization
	protected void Start () {
        sprite = GetComponent<SpriteRenderer>();
	}

    // Damage this block
    protected virtual void Damage() { }
}
