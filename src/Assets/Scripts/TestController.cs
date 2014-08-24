using UnityEngine;
using System.Collections;

public class TestController : MonoBehaviour {
	public float speed;
	public WebManager manager;

	private Vector3 newPos;
	private bool modeUnzoom = false;
	private bool modeZoom = false;
	// Use this for initialization
	void Start () {
		newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		checkInputs();
		//Camera.main.orthographicSize = Mathf.Clamp(rigidbody2D.velocity.magnitude/1.5f, 15f, 20f);
		if(modeUnzoom){
			Camera.main.orthographicSize += Time.deltaTime * 5f;
		}else if(modeZoom){
			Camera.main.orthographicSize -= Time.deltaTime * 5f;
		}


	}

	void checkInputs(){
		//newPos.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
		//newPos.y += speed * Input.GetAxis("Vertical") * Time.deltaTime;
		//transform.position = newPos;
		if((Input.GetAxis("Horizontal") != 0f ||
		   Input.GetAxis("Vertical") != 0f) &&
		   !SoundManager.Get().clips[3].isPlaying && !modeUnzoom && !modeZoom){
			SoundManager.Get().Play(3);
		}
		rigidbody2D.AddForce(new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, speed * Input.GetAxis("Vertical") * Time.deltaTime));

	}

	void OnCollisionEnter2D(Collision2D other) {
		SoundManager.Get().Play(1);
		manager.web -= other.relativeVelocity.magnitude+other.rigidbody.mass/1000f;
	}

	IEnumerator DestroyC(){
		yield return new WaitForSeconds(3f);
		transform.position = newPos;
		modeUnzoom = false;
		modeZoom = true;
		StartCoroutine("Spawn");
	}

	IEnumerator Spawn(){
		yield return new WaitForSeconds(3f);
		SoundManager.Get().Play(5);
		renderer.enabled = true;
		collider2D.enabled = true;
		modeZoom = false;
		manager.score = 0;
	}

	public void Destroy(){
		renderer.enabled = false;
		collider2D.enabled = false;
		SoundManager.Get().Play(2);
		StartCoroutine("DestroyC");
		modeUnzoom = true;
	}
}
