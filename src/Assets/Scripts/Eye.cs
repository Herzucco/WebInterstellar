using UnityEngine;
using System.Collections;

public class Eye : MonoBehaviour {
	public TestController player;
	public float margin;
	public Vector3 basePos;
	private Vector3 distance;
	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if(player == null){
			GameObject p = GameObject.FindGameObjectWithTag("Player");
			if(p != null){
				player = p.GetComponent<TestController>();
			}
		}else{
			//float angle = Vector3.Angle(transform.position, player.transform.position);
			distance = player.transform.position - transform.position;
			distance = Vector3.ClampMagnitude( distance, margin);
			transform.localPosition = basePos + distance;
		}

	}
}
