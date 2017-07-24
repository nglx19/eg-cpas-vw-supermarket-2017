using UnityEngine;
using System.Collections;

public class ControllerLogic : MonoBehaviour {

	public GameObject[] speedIndicators;
	private int speed;

	void increaseSpeed() {
		if (speed < speedIndicators.Length) speed++;
		updateIndicator (speed);
	}

	void decreaseSpeed() {
		if (speed > 1) speed--;
		updateIndicator (speed);
	}

	void updateIndicator(int speed) {
		for (int i = 0; i < speedIndicators.Length; i++) {
			GameObject speedIndicator = speedIndicators[i];
			speedIndicator.SetActive(i < speed);
		}		
	}

	// Use this for initialization
	void Start () {
		speed = 1;
		updateIndicator (speed);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			decreaseSpeed ();
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			increaseSpeed ();
		}
	}
}
