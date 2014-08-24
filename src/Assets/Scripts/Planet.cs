using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Planet : MonoBehaviour {
	public CircleCollider2D circle;
	public TestPush push;
	public DistanceJoint2D joint;
	public float life = 1000f;
	public int nbAsteroids = 16;
	public int type;
	public Asteroid asteroid;
	public PlanetConnector connector;
	// Use this for initialization
	void Start () {
		//slider = gameObject.GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(joint != null && joint.connectedBody == null){
			joint.enabled = false;
		}
		if(life <= 0f){
			Destroy(gameObject);
		}
	}

	public void Setup(Star star, int size, int i){
		if(star != null){
			float radius = Random.Range(1f, (float) size/2f);
			//circle.radius = radius;
			transform.localScale = new Vector3(radius, radius, 0f);
			rigidbody2D.mass = radius*1000f;
			life = rigidbody2D.mass;
			push.force.y = Random.Range(1f, 30f);
			joint.connectedBody = star.rigidbody2D;
			joint.distance = Vector3.Distance(star.transform.position, transform.position);
			joint.enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "ennemy" || other.gameObject.tag == "Player" ||
		    other.gameObject.tag == "planet" || other.gameObject.tag == "star" ||
		    other.gameObject.tag == "planet-attached" || other.gameObject.tag == "asteroid"){
			life -= other.relativeVelocity.magnitude*other.rigidbody.mass/100f;

			if(life <= 0f){
				PopAsteroids(rigidbody2D.velocity);
				if(gameObject.renderer.isVisible){
					SoundManager.Get().Play(0);
					Shaker.Shake(1f);
				}

			}else{
				if(gameObject.renderer.isVisible){
					SoundManager.Get().Play(1);
					Shaker.Shake(0.1f);
				}
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
			aster.transform.localScale = new Vector3(transform.localScale.x/nbAsteroids*5, transform.localScale.y/nbAsteroids*5, 1f);
			aster.transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
			aster.rigidbody2D.velocity = velocity;
			aster.transform.parent = transform.parent;
			//aster.rigidbody2D.AddForce(new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f)));
		}
	}
}
