using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour {
	public Camera camera;
	public float shake = 0f;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1f;
	public static Shaker shaker;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shake > 0f) {
			Vector3 shaking = Random.insideUnitSphere * shakeAmount;
			shaking.z = -10;
			camera.transform.localPosition = shaking;
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0f;
		}
	}

	public static void Shake(float time, float amount){
		if(shaker == null) shaker = GameObject.FindGameObjectWithTag("shaker").GetComponent<Shaker>();

		shaker.shake = time;
		shaker.shakeAmount = amount;
	}

	public static void Shake(float time){
		if(shaker == null) shaker = GameObject.FindGameObjectWithTag("shaker").GetComponent<Shaker>();
		
		shaker.shake = time;
	}
}
