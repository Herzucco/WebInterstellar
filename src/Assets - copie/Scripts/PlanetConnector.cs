using UnityEngine;
using System.Collections;

public class PlanetConnector : MonoBehaviour {
	public GameObject player;
	public bool linked = false;
	public Vector2 newVel;
	// Use this for initialization
	void Start () {
		newVel = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
	}
	
	// Update is called once per frame
	void Update () {
		if(linked){
			if(Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f){
				newVel = rigidbody2D.velocity;
				newVel.x /= 2f;
				newVel.y /= 2f;
				rigidbody2D.velocity = newVel;
			}
			if(Input.GetMouseButtonDown(1)){
				DistanceJoint2D joint = GetComponent<DistanceJoint2D>();
				joint.enabled = false;
				linked = false;
				tag = "planet";
			}
		}
	}

	public void Connect(Rigidbody2D attach, float distance){
		DistanceJoint2D joint = GetComponent<DistanceJoint2D>();
		joint.enabled = true;
		joint.connectedBody = attach;
		joint.distance = distance;
		linked = true;
	}
}
