using UnityEngine;
using System.Collections;

public class TestPush : MonoBehaviour {
	public Vector2 force;
	public SpringJoint2D joint;
	// Use this for initialization
	void Start () {
		rigidbody2D.AddForce(force);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
