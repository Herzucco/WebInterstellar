using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestConnector : MonoBehaviour {
	public TestLinker lastLinker;
	public TestLinker prefabLinker;
	public List<TestLinker> linkerList;
	public B2Dragger dragger;
	public HingeJoint2D joint;
	public GameObject player;
	public float distance;
	public float minimumGap;
	public float maxDistance;
	public bool linked = false;
	public GameObject selfPrefab;
	public WebManager manager;
	// Use this for initialization
	void Start () {
		linkerList = new List<TestLinker>();
		distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y) , new Vector2(player.transform.position.x, player.transform.position.y));
		GameObject wm = GameObject.FindGameObjectWithTag("webManager");
		manager = wm.GetComponent<WebManager>();
		CreateRope();
	}
	
	// Update is called once per frame
	void Update () {
		if(player == null ||
		   Vector3.Distance(player.transform.position, transform.position) > maxDistance ||
		   (!linked && Vector3.Distance(player.transform.position, transform.position) > manager.web) ||
		   linked && Input.GetMouseButtonDown(1)){
			GameObject.Destroy(gameObject);
			DestroyRope();
			return;
		}
		distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y) , new Vector2(player.transform.position.x, player.transform.position.y));
		CreateRope();
		if(Input.GetMouseButtonUp(0)){
			if(linked){
				dragger.enabled = false;
				rigidbody2D.mass = 100f;
				rigidbody2D.drag = 0f;
			}else{
				DestroyRope();
				GameObject.Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "planet" && !linked && Vector3.Distance(player.transform.position, transform.position) <= manager.web){
			linked = true;
			other.gameObject.tag = "planet-attached";
			GameObject connector = (GameObject) GameObject.Instantiate(selfPrefab,  new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			TestConnector instance = connector.GetComponent<TestConnector>();
			instance.player = other.gameObject;
			connector.GetComponent<B2Dragger>().player = other.transform;
			joint.connectedBody = other.rigidbody2D;
			joint.enabled = true;
			dragger.enabled = false;
			other.gameObject.GetComponent<PlanetConnector>().Connect(player.rigidbody2D, linkerList.Count * minimumGap);

			manager.web -= linkerList.Count*other.rigidbody2D.mass/1000f;
			player = other.gameObject;
		}
	}

	public void DestroyRope(){
		for(int i = 0; i < linkerList.Count; i++){
			GameObject.Destroy(linkerList[i].gameObject);
			//linkerList.RemoveAt(i);
		}
	}

	public void CreateRope(){
		float modulo = Mathf.Round(distance / minimumGap);

		if(modulo > linkerList.Count && !linked){
			if(lastLinker != null){
				lastLinker.DestroyLinkToChild();
			}
			for(int i = linkerList.Count; i <= modulo; i++){
				if(manager.web-linkerList.Count <= 0){
					break;
				}
				TestLinker linker = (TestLinker) GameObject.Instantiate(prefabLinker, player.transform.position, Quaternion.identity);
				//TestLinker linker = link.GetComponent<TestLinker>();
				
				if(linkerList.Count == 0){
					linker.parent = gameObject;
				}else{
					linker.parent = linkerList[linkerList.Count-1].gameObject; //last
				}
				
				if(i == modulo){
					linker.child = player;
					lastLinker = linker;
				}
				
				linkerList.Add(linker);
			}
		}

	}
}
