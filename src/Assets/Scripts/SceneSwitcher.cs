using UnityEngine;
using System.Collections;

public class SceneSwitcher : MonoBehaviour {
	public bool goHowTo = false;
	public bool goMenu = false;
	public float speed;
	public float time;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(goHowTo){
			Camera.main.transform.Translate(new Vector3(0f, -speed*Time.deltaTime, 0f));
		}else if(goMenu){
			Camera.main.transform.Translate(new Vector3(0f, speed*Time.deltaTime, 0f));
		}
	}

	public void Play(){
		Application.LoadLevel("Main");
	}

	public void HowTo(){
		goHowTo = true;
		goMenu = false;
		SoundManager.Get().Play(5);
		StartCoroutine("StopTravelling");
	}

	public void Menu(){
		goMenu = true;
		goHowTo = false;
		SoundManager.Get().Play(5);
		StartCoroutine("StopTravelling");
	}

	IEnumerator StopTravelling(){
		yield return new WaitForSeconds(time);
		goHowTo = false;
		goMenu = false;
	}
}
