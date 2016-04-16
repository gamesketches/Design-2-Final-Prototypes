using UnityEngine;
using System.Collections;

public class bubbleBehavior : MonoBehaviour {

	public float maxSize = 5f;
	public float duration = 5f;
	private float t = 0f;
	private Color bubbleColor;
	private bool moving = false;
	// Use this for initialization
	void Start () {
		bubbleColor = Random.value > 0.5 ? Color.red : Color.white;
		gameObject.GetComponent<Renderer>().material.color = bubbleColor;
	}
	
	// Update is called once per frame
	void Update () {
		if(!moving) {
			//StartCoroutine(move(Random.Range(-10.0f, 10.0f)));
		}
		Vector3 baseScale = new Vector3(0f,0f,0f);
		Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);
		gameObject.transform.localScale = Vector3.Lerp(baseScale, maxScale, t);
		t += duration * Time.deltaTime;
	}

	public Color GetColor() {
		return bubbleColor;
	}

	IEnumerator move(float dir) {
		moving = true;
		float target = transform.position.x + dir;
		Vector3 oldPos = transform.position;
		Vector3 newPos = transform.position;
		newPos.x += dir;
		float time = 0f;
		while( time != 1f) {
			transform.position = Vector3.Lerp(oldPos, newPos, t);
			t += dir * Time.deltaTime;
			yield return null;
		}
		moving = false;
	}
}
