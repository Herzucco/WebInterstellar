using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Universe : MonoBehaviour {
	public Rect size;

	public int nbSystems;
	public int nbStars;

	public float starsDelay;
	public float systemsDelay;

	public int minSystemSize;
	public int maxSystemSize;
	public int minSystemPlanets;
	public int maxSystemPlanets;

	public GameObject star;
	public SystemGenerator systemPrefab;

	public List<SystemGenerator> systems;
	public int nbStarsDone;
	// Use this for initialization
	void Start () {
		systems = new List<SystemGenerator>();
		StartCoroutine("InstanciateStars");
		StartCoroutine("InstanciateSystems");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator InstanciateStars()
	{
		GameObject stars = new GameObject();
		stars.name = "stars";
		stars.transform.parent = transform;
		for(int i = 0; i < nbStars; i++){
			Vector3 position = new Vector3(transform.position.x+Random.Range(-size.width, size.width), transform.position.y+Random.Range(-size.height, size.height), 0f);
			
			GameObject s = (GameObject) GameObject.Instantiate(star, position, Quaternion.identity);
			s.transform.parent = stars.transform;
			nbStarsDone += 1;
			yield return new WaitForSeconds(starsDelay);
		}
	}

	IEnumerator InstanciateSystems()
	{
		for(int i = 0; i < nbSystems; i++){
			Vector3 position = new Vector3(transform.position.x+Random.Range(-size.width, size.width), transform.position.y+Random.Range(-size.height, size.height), 0f);
			
			SystemGenerator system = (SystemGenerator) GameObject.Instantiate(systemPrefab, position, Quaternion.identity);
			system.size = Random.Range(minSystemSize, maxSystemSize);
			system.nbPlanets = Random.Range(minSystemPlanets, maxSystemPlanets);
			system.transform.parent = transform;
			system.Generate();
			systems.Add(system);
			yield return new WaitForSeconds(systemsDelay);
		}
	}
}
