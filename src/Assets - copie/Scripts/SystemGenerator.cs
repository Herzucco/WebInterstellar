using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemGenerator : MonoBehaviour {
	public int nbPlanets;
	public int size;
	public Planet star;
	public List<Planet> planets;
	public Planet[] prefabs;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate(){
		Planet star = (Planet) GameObject.Instantiate(prefabs[0], transform.position, Quaternion.identity);
		star.Setup(null, size, 0);
		star.rigidbody2D.mass = 100000f;
		star.gameObject.transform.parent = transform;
		for(int i = 0; i < nbPlanets; i++){
			int index = Random.Range(0, prefabs.Length-1);
			Vector3 position = new Vector3(transform.position.x+size*(i+1), transform.position.y, 0f);
			
			Planet planet = (Planet) GameObject.Instantiate(prefabs[index], position, Quaternion.identity);
			planet.Setup(star, size, i+1);
			planet.gameObject.transform.parent = transform;
			
		}
	}
}
