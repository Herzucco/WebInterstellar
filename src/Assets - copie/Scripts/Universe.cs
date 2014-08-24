using UnityEngine;
using System.Collections;

public class Universe : MonoBehaviour {
	public Rect size;

	public int nbSystems;
	public int nbStars;

	public int minSystemSize;
	public int maxSystemSize;
	public int minSystemPlanets;
	public int maxSystemPlanets;

	public GameObject star;
	public SystemGenerator systemPrefab;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < nbStars; i++){
			Vector3 position = new Vector3(transform.position.x+Random.Range(-size.width, size.width), transform.position.y+Random.Range(-size.height, size.height), 0f);

			GameObject s = (GameObject) GameObject.Instantiate(star, position, Quaternion.identity);
			s.transform.parent = transform;
		}
		for(int i = 0; i < nbSystems; i++){
			Vector3 position = new Vector3(transform.position.x+Random.Range(-size.width, size.width), transform.position.y+Random.Range(-size.height, size.height), 0f);
			
			SystemGenerator system = (SystemGenerator) GameObject.Instantiate(systemPrefab, position, Quaternion.identity);
			system.size = Random.Range(minSystemSize, maxSystemSize);
			system.nbPlanets = Random.Range(minSystemPlanets, maxSystemPlanets);
			system.transform.parent = transform;
			system.Generate();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
