using UnityEngine;
using System.Collections;

public class TestJoint : MonoBehaviour {
	public TestConnector connectorPrefab;
	public GameObject connector;
	private bool clicking = false;
	private TestConnector instance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			clicking = true;

			Vector3 click = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			connector =  (GameObject) GameObject.Instantiate(connectorPrefab.gameObject,  new Vector3(click.x, click.y, 0), Quaternion.identity);
			instance = connector.GetComponent<TestConnector>();
			instance.player = gameObject;
			connector.GetComponent<B2Dragger>().player = transform;
			//create connector here
			//connector has event listener on collision 2d
			//if connector collides planet => stick it
			//if connector collides spider => stick it

		}

	}
}
