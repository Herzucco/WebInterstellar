using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {
	public float speed;

	private Vector3 newPos;
	// Use this for initialization
	void Start () {
		newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		checkInputs();
	}

	void checkInputs(){
		//newPos.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
		//newPos.y += speed * Input.GetAxis("Vertical") * Time.deltaTime;
		//transform.position = newPos;
		rigidbody2D.AddForce(new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime));

	}
}
