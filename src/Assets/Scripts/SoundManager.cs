using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	public AudioSource[] clips;
	public static SoundManager manager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play(int index){
		clips[index].enabled = true;
		clips[index].Play();
	}

	public static SoundManager Get(){
		if(manager == null) manager = GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundManager>();

		return manager;
	}
}
