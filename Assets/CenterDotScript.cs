using UnityEngine;
using System.Collections;

public class CenterDotScript : MonoBehaviour {

	float timeTilMove;
	bool touched;
	public bool traveling;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
		touched = true;
		traveling = false;
		timeTilMove = Random.Range(2, 10);
		Debug.Log(timeTilMove);
	}
	
	// Update is called once per frame
	void Update () {
		if(!traveling) {
			timeTilMove -= Time.deltaTime;
			if(timeTilMove <= 0f) {
				targetPos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), -1);
				StartCoroutine(moveDot(gameObject.transform.position));
			}
		}
		if(Input.GetMouseButtonDown(0)){
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if(hitInfo)
			{
				if( hitInfo.collider.transform.gameObject.tag == "bubble") {
					traveling = false;
					gameObject.transform.position = new Vector3(0, 0, -1);
				}
			}
		}
	}

	IEnumerator moveDot(Vector3 startPos) {
		float t = 0f;
		traveling = true;
		Debug.Log("started");
		while(t <= 1f && traveling) {
			gameObject.transform.position = Vector3.Lerp(startPos, targetPos, t);
			t += Time.deltaTime;
			yield return null;
		}
		if(Vector3.Distance(new Vector3(0f, 0f, -1f), gameObject.transform.position) > 4.5) {
			GetComponent<AudioSource>().Play();
		}
		else {
			traveling = false;
			timeTilMove = Random.Range(0, 5);
		}
	}


}
