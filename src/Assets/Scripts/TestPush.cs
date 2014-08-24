using UnityEngine;
using System.Collections;

public class TestPush : MonoBehaviour {
	public Vector2 force;
	public SpringJoint2D joint;
	// Use this for initialization
	void Start () {
		Push();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Push(){
		rigidbody2D.velocity = force;
	}

}
