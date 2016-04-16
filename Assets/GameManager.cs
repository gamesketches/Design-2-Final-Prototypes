using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float timeLimit = 10f;
	public int timeBetween;
	private int timer;
	private int redScore;
	private int whiteScore;
	public Text decidingText;
	string[] resultStrings;
	// Use this for initialization
	void Start () {
		timer = 0;
		redScore = 0;
		whiteScore = 0;
		resultStrings = new string[] {"Move Foward", "Move Right", "Move Left", "Go Back"}; 
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
		timeLimit -= Time.deltaTime;
		if(timer >= timeBetween && timeLimit > 0.0f) {
			float randX = Random.value * 20.0f - 10f;
			float randY = Random.value * 20.0f - 10f;
			Instantiate(Resources.Load("Bubble"), new Vector3(randX, randY, 0f), Quaternion.identity);
		}	
		else if(timeLimit <= 0f) {
			if(redScore + whiteScore > 30) {
				decidingText.text = resultStrings[0];
			}
			if(redScore > whiteScore && redScore > 15) {
				decidingText.text = resultStrings[1];
			}
			else if(whiteScore > 15){
				decidingText.text = resultStrings[2];
			}
			else {
				decidingText.text = resultStrings[3];
			}
			foreach(GameObject bubble in GameObject.FindGameObjectsWithTag("bubble")){
				Destroy(bubble);
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
			if(hitInfo)
			{
				if( hitInfo.collider.transform.gameObject.tag == "bubble") {
					Color bubbleColor = hitInfo.collider.transform.gameObject.GetComponent<bubbleBehavior>().GetColor();
					if(bubbleColor == Color.red) {
						redScore++;
					}
					else {
						whiteScore++;
					}
					Destroy(hitInfo.transform.gameObject);
				}
				// Here you can check hitInfo to see which collider has been hit, and act appropriately.
			}
			else {
				Debug.Log(pos);
			}
		}
		if(Input.GetKeyDown(KeyCode.R)) {
			foreach(GameObject bubble in GameObject.FindGameObjectsWithTag("bubble")){
				Destroy(bubble);
			}
			decidingText.text = "";
			timer = 0;
			timeLimit = 10f;
			redScore = 0;
			whiteScore = 0;
			if(Random.value < 0.2) {
				Debug.Log("shuffle");
				resultStrings = ShuffleArray(resultStrings);
				Debug.Log("done shuffling");
			}
		}
	}

	string[] ShuffleArray(string[] resultStrings) {
		for(int i = 0; i < resultStrings.Length; i++) {
			string tmp = resultStrings[i];
			int k = Random.Range(0, 4);
			resultStrings[i] = resultStrings[k];
			resultStrings[k] = tmp;
		}

		return resultStrings;
	}
}
