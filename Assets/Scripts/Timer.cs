using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerText;
	float startTime;
	string hourTime;

	void Start() {
		startTime = Time.time;
	}

	void Update() {
		float t = Time.time - startTime;

		string hours = ((int)t / 60).ToString ();
		if ((int)t / 60 <= 9) {
			hourTime = "0" + hours;
		} else {
			hourTime = hours;
		}
		timerText.text = hourTime + ":00";
	}

}
