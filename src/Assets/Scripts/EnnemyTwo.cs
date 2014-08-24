using UnityEngine;
using System.Collections;

public class EnnemyTwo : MonoBehaviour {
	public Planet planet;
	public float delay;
	public float minDistance;
	public TestController player;
	public float margin;
	public float forceMultiplier;

	private Vector2 force = new Vector2(0f,0f);
	private Vector3 distance;
	// Use this for initialization
	void Start () {	
		StartCoroutine("Shoot");
		rigidbody2D.mass = transform.localScale.x * 1000f;
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null){
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			if(p != null){
				player = p.GetComponent<TestController>();
			}
		}
	}

	IEnumerator Shoot(){
		while (true)
		{   
			if(player != null){
				float dis = Vector3.Distance(player.transform.position, transform.position);
				if(dis <= minDistance){
					distance = player.transform.position - transform.position;
					
					force.x = distance.x * forceMultiplier;
					force.y = distance.y * forceMultiplier;
					
					distance = Vector3.ClampMagnitude( distance, margin * transform.localScale.x);
					distance.x = transform.position.x + distance.x;
					distance.y = transform.position.y + distance.y;
					Planet pl = (Planet) GameObject.Instantiate(planet, distance, Quaternion.identity);
					pl.transform.localScale = transform.localScale/10f;
					pl.rigidbody2D.mass = pl.transform.localScale.x * 1000f;
					pl.life = pl.rigidbody2D.mass;
					pl.rigidbody2D.AddForce(force);
				}
			}


			yield return new WaitForSeconds(delay);
		}   
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "planet" || other.gameObject.tag == "planet-attached"){
			Planet pl = other.gameObject.GetComponent<Planet>();
			if(pl.type != planet.type){
				if(rigidbody2D.mass <= other.rigidbody.mass){
					Ennemy en = gameObject.GetComponent<Ennemy>();

					en.life = 0;
					en.PopAsteroids(rigidbody2D.velocity);
					SoundManager.Get().Play(0);
				}else{
					rigidbody2D.mass -= other.rigidbody.mass;
					transform.localScale = new Vector3(transform.localScale.x - other.transform.localScale.x,
					                                   transform.localScale.y - other.transform.localScale.y);
				}
			}else{
				pl.life = 0;
				rigidbody2D.mass += other.rigidbody.mass;
				transform.localScale = new Vector3(transform.localScale.x + other.transform.localScale.x,
				                                   transform.localScale.y + other.transform.localScale.y);
			}
		}	
	}
}