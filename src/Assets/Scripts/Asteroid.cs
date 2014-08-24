using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	public CircleCollider2D collider;
	public SpriteRenderer sprite;
	public float lifeTime = 20f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0f){
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible() {
		collider.enabled = false;
		//sprite.enabled = false;
		rigidbody2D.Sleep();
	}

	void OnBecameVisible() {
		collider.enabled = true;
		//sprite.enabled = true;
		rigidbody2D.WakeUp();
	}
}
