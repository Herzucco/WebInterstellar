using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WebManager : MonoBehaviour {
	public float web;
	public float maxWeb;
	private float baseWeb;
	public float score;
	public Text loading;
	public Text scoreText;
	public Slider slider;
	public Slider webSlide;
	public TestController player;
	public Text text;
	public Universe universe;
	public bool loadingDone = false;

	private float lastWeb;
	// Use this for initialization
	void Start () {
		slider.maxValue = universe.nbSystems+universe.nbStars;
		webSlide.maxValue = maxWeb;
		webSlide.animation.Stop();
		lastWeb = web;
		baseWeb = web;
	}
	
	// Update is called once per frame
	void Update () {
		if(web >= maxWeb) web = maxWeb;
		if(web <= 0f){
			player.Destroy();
			web = baseWeb;
		}
		if(web < lastWeb){
			if(!webSlide.animation.isPlaying){
				webSlide.animation.Play();
			}
		}
		text.text = "Score : "+Mathf.Round(score);
		webSlide.value = web;
		//text.text = "Web : "+Mathf.Floor(web);
		slider.value = universe.systems.Count+universe.nbStarsDone;

		if(!loadingDone && slider.value >= slider.maxValue){
			slider.gameObject.SetActive(false);
			loading.gameObject.SetActive(false);
			text.gameObject.SetActive(true);
			player.gameObject.SetActive(true);
			webSlide.gameObject.SetActive(true);
			Camera.main.transform.parent = player.transform;
			loadingDone = true;
			//SoundManager.Get().Play(4);
		}

		lastWeb = web;
	}
}
