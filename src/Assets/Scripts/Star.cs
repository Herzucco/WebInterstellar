using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {
	public int type;
	public Planet ownP;
	public WebManager manager;
	// Use this for initialization
	void Start () {
		GameObject wm = GameObject.FindGameObjectWithTag("webManager");
		manager = wm.GetComponent<WebManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup(int size){
		float radius = Random.Range(2f, 5f);
		transform.localScale = new Vector3(radius, radius, 0f);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "planet" || other.gameObject.tag == "planet-attached"){
			Planet planet = other.gameObject.GetComponent<Planet>();
			if(planet.type == type){
				planet.life = 0;
				rigidbody2D.mass += other.rigidbody.mass;
				ownP.life += other.rigidbody.mass;
				transform.localScale = new Vector3(transform.localScale.x + other.transform.localScale.x,
				                                   transform.localScale.y + other.transform.localScale.y);
				if(manager != null){
					manager.score += Mathf.Round(rigidbody2D.mass/1000f);
					manager.web += Mathf.Round(rigidbody2D.mass/1000f);
				}
			}
		}	
	}
}
