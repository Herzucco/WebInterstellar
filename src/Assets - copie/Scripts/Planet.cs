using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	public CircleCollider2D circle;
	public TestPush push;
	public DistanceJoint2D joint;
	public float life = 1000f;
	public int nbAsteroids = 16;

	public Asteroid asteroid;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(joint.connectedBody == null){
			joint.enabled = false;
		}
		if(life <= 0f){
			Destroy(gameObject);
		}
	}

	public void Setup(Planet star, int size, int i){
		float radius = Random.Range(1f, (float) size/2f);
		//circle.radius = radius;
		transform.localScale = new Vector3(radius, radius, 0f);


		if(star != null){
			push.force.y = Random.Range(1000f, 10000f);
			joint.connectedBody = star.rigidbody2D;
			joint.distance = Vector3.Distance(star.transform.position, transform.position);
			joint.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "planet" || other.gameObject.tag == "planet-attached" || other.gameObject.tag == "asteroid"){
			life -= other.relativeVelocity.magnitude;
			if(life <= 0f){
				PopAsteroids(rigidbody2D.velocity);
			}
		}	
	}

	public void PopAsteroids(Vector2 velocity){
		for(int i = 0; i < nbAsteroids; i++){
			Asteroid aster = (Asteroid) GameObject.Instantiate(asteroid, new Vector3(Random.Range(transform.position.x-5f,
			                                                                                      transform.position.x+5f),
			                                                                         Random.Range(transform.position.y-5f,
			             																		transform.position.y+5f),
			                                                                         0f), transform.rotation);
			aster.transform.localScale = new Vector3(transform.localScale.x/nbAsteroids*10, transform.localScale.y/nbAsteroids*10, 1f);
			aster.transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
			aster.rigidbody2D.velocity = velocity;
			//aster.rigidbody2D.AddForce(new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f)));
		}
	}
}
