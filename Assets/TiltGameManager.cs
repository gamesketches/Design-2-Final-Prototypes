using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TiltGameManager : MonoBehaviour {

	private Dictionary<Vector3, AudioClip> directions;
	private Vector3 targetVector;
	AudioSource audio;
	public Vector2 redVector = new Vector2(0.25f, 0.0f);
	public Vector2 greenVector = new Vector2(0.0f, 0.25f);
	public Vector2 neutralVector = new Vector2(0.15f, 0.15f);
	public bool thomas;
	public bool joke;
	private AudioClip[] thomasClips;
	private int thomasClipsIter = 0;
	SpriteRenderer targetSprite;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		directions = new Dictionary<Vector3, AudioClip>();
		//directions.Add(new Vector3(0.25f, 0.25f), Resources.Load<AudioClip>("neutral"));
		directions.Add(redVector, Resources.Load<AudioClip>("red"));
		directions.Add(greenVector, Resources.Load<AudioClip>("green"));
		if(thomas) {
			if(joke) {
				thomasClips = Resources.LoadAll<AudioClip>("joke");
			}
			else {
				thomasClips = Resources.LoadAll<AudioClip>("dylanThomas");
			}
			audio.clip = thomasClips[thomasClipsIter];
		}
		else {
			audio.clip = directions[targetVector];
		}
		targetSprite = GameObject.FindGameObjectWithTag("bubble").GetComponent<SpriteRenderer>();
		targetSprite.color = Color.red;
		targetVector = redVector;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		//if(targetVector.x == targetVector.y) {
		if(targetVector.x == neutralVector.x) {
			if(Mathf.Abs(Input.acceleration.x) < neutralVector.x &&
				Mathf.Abs(Input.acceleration.y) < neutralVector.y) {
				List<Vector3> temp = new List<Vector3>(directions.Keys);
				targetVector = temp[Random.Range(0, temp.Count)];
				if(targetVector.x == greenVector.x) { 
					targetSprite.color = Color.green;
				}
				else {
					targetSprite.color = Color.red;
				}
				if(thomas) {
					//thomasClipsIter++;
					//audio.clip = thomasClips[thomasClipsIter];
				}
				else {
					audio.clip = directions[targetVector];
				}
				if(!audio.isPlaying) {
					audio.Play();
				}
			}
		}
		else if(targetVector.x == greenVector.x){
			if(Mathf.Abs(Input.acceleration.y) > greenVector.y) {
				targetVector = neutralVector;
				if(thomas) {
					if(!audio.isPlaying) {
						thomasClipsIter++;
						audio.clip = thomasClips[thomasClipsIter];
					}
				}
				else {
					audio.clip = Resources.Load<AudioClip>("neutral");
					audio.Play();
				}
				Debug.Log("going to neutral");
			}
		}
		else if(targetVector.x == redVector.x){
			if(Mathf.Abs(Input.acceleration.x) > redVector.x){
				targetVector = neutralVector;
				if(thomas) {
					if(!audio.isPlaying) {
						thomasClipsIter++;
						audio.clip = thomasClips[thomasClipsIter];
					}
				}
				else {
					audio.clip = Resources.Load<AudioClip>("neutral");
					audio.Play();
				}
			}
		}
		
		Camera.main.backgroundColor = new Color(Mathf.Abs(Input.acceleration.x) * 1.4f, Mathf.Abs(Input.acceleration.y) * 1.4f, 0);
	}
}
