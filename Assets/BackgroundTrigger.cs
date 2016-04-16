using UnityEngine;
using System.Collections;

public class BackgroundTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerExit(Collider other) {
		Debug.Log(other.gameObject);
	}
}
