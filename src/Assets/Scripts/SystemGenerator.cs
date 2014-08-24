using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemGenerator : MonoBehaviour {
	public int nbPlanets;
	public int size;
	public float rate = 10f;
	public Planet star;
	public List<Planet> planets;

	public Planet[] prefabs;
	public Star[] stars;
	public bool activated = false;
	public Ennemy[] ennemies;
	public Ennemy ennemy;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Generate(){
		int index = Random.Range(0, stars.Length);
		Star star = (Star) GameObject.Instantiate(stars[index], transform.position, Quaternion.identity);
		star.Setup(size);
		star.gameObject.transform.parent = transform;
		star.gameObject.SetActive(false);
		planets.Add(star.GetComponent<Planet>());

		for(int i = 0; i < nbPlanets; i++){
			index = Random.Range(0, prefabs.Length);
			Planet prefab = null;



			Vector3 position = new Vector3(transform.position.x+10f+size*(i+1)*2, transform.position.y, 0f);
			
			Planet planet = (Planet) GameObject.Instantiate(prefabs[index], position, Quaternion.identity);
			planet.Setup(star, size, i+1);
			planet.gameObject.transform.parent = transform;
			planet.gameObject.SetActive(false);
			planets.Add(planet);
		}

		float testEnnemy = Random.Range(0f, 100f);
		if(testEnnemy > 100f-rate*size){
			Vector3 position = new Vector3(transform.position.x-10f-size, transform.position.y, 0f);

			index = Random.Range(0, ennemies.Length);
			ennemy = (Ennemy) GameObject.Instantiate(ennemies[index], position, Quaternion.identity);
			ennemy.Setup();
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && !activated){
			for(int i =0; i < planets.Count; i++){
				if(planets[i] != null && ((planets[i].connector != null && planets[i].connector.inSystem)
				   || planets[i].connector == null)){
					planets[i].gameObject.SetActive(true);
					if(planets[i].push != null){
						planets[i].push.Push();
					}
				} 
			}
			//if(ennemy != null) ennemy.gameObject.SetActive(true);
			activated = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && activated){
			for(int i =0; i < planets.Count; i++){
				if(planets[i] != null && ((planets[i].connector != null && planets[i].connector.inSystem)
				   || planets[i].connector == null)) planets[i].gameObject.SetActive(false);
			}
			//if(ennemy != null) ennemy.gameObject.SetActive(false);
			activated = false;
		}
	}
}
