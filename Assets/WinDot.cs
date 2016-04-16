using UnityEngine;
using System.Collections;

public class WinDot : MonoBehaviour {

	public GameObject otherDot;
	private int chances = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if(hitInfo)
			{
				if( hitInfo.collider.transform.gameObject.name == "WinDot" && chances < 3) {
					GetComponent<AudioSource>().Play();
					Destroy(otherDot);
				}
				else if( hitInfo.collider.transform.gameObject.tag == "bubble") {
					CenterDotScript otherDotScript = hitInfo.collider.gameObject.GetComponent<CenterDotScript>();
					otherDotScript.traveling = false;
					hitInfo.collider.gameObject.transform.position = new Vector3(0, 0, -1);
				}
				else if(hitInfo.collider.transform.gameObject.name == "Background"){
					}
				}
			else{
				chances++;
				Debug.Log(chances);
				if(chances > 2) {
					otherDot.GetComponent<AudioSource>().Play();
				}

			}

		}
	}
}
