using UnityEngine;
using System.Collections;

public class TestLinker : MonoBehaviour {
	public ArrayList linkerList;
	public GameObject parent;
	public GameObject child;
	public HingeJoint2D jointToLinker;
	public HingeJoint2D jointToPlayer;
	public DistanceJoint2D jointPostToLinker;
	public DistanceJoint2D jointPostToPlayer;
	// Use this for initialization
	void Start () {
		jointToLinker.connectedBody = parent.rigidbody2D;

		if(child != null){
			jointToPlayer.enabled = true;
			jointToPlayer.connectedBody = child.rigidbody2D;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DestroyLinkToChild(){
		jointToPlayer.enabled = false;
	}

	public void PostJoints(){
		jointPostToLinker.connectedBody = jointToLinker.connectedBody;
		jointPostToLinker.enabled = true;
		jointToLinker.enabled = false;

		jointPostToPlayer.connectedBody = jointToPlayer.connectedBody;
		if(jointToPlayer.enabled){
			jointToPlayer.enabled = false;
			jointPostToPlayer.enabled = true;
		}
	}
}
