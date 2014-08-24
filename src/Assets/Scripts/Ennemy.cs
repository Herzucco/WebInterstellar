using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour {
	public float life;
	public Asteroid asteroid;
	
	public int nbAsteroids = 16;
	public Slider slider;
	public WebManager manager;
	public float score;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("webManager").GetComponent<WebManager>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = life;
		if(life <= 0f){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "planet" || other.gameObject.tag == "star" || other.gameObject.tag == "planet-attached" || other.gameObject.tag == "asteroid"){
			life -= other.relativeVelocity.magnitude*10f*(other.rigidbody.mass/100f);
			if(life <= 0f){
				manager.score += score;
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
			aster.transform.localScale = new Vector3(transform.localScale.x/nbAsteroids, transform.localScale.y/nbAsteroids, 1f);
			aster.transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 360f)));
			aster.rigidbody2D.velocity = velocity;
			aster.transform.parent = transform.parent;
			//aster.rigidbody2D.AddForce(new Vector2(Random.Range(-1000f, 1000f), Random.Range(-1000f, 1000f)));
		}
	}

	public void Setup(){
		//gameObject.SetActive(false);
		
		float scale = Random.Range(10f, 30f);
		transform.localScale = new Vector3(scale, scale, 1f);
		rigidbody2D.mass = transform.localScale.x*1000f;
		life = rigidbody2D.mass*10f;
		slider.maxValue = life;
		score = life;
	}
}
