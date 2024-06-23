using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour {

	public Text score;
	public Text highScore;
	public Text time;
	public Text hits;
	public Text miss;

	// Use this for initialization
	void Start () {

		score.text = StaticArrays.currentScore.ToString ();
		highScore.text = StaticArrays.currentModeHighScore.ToString ();
		time.text = StaticArrays.currentTime.ToString();
		hits.text = StaticArrays.currentHits.ToString ();
		miss.text = StaticArrays.currentMisses.ToString ();

		if (StaticArrays.currentScore > StaticArrays.currentModeHighScore)
		{
			if(StaticArrays.currentMode == "relaxed")
				PlayerPrefs.SetInt(StaticArrays.relaxedModeHighScoreKey,StaticArrays.currentScore);
			else if (StaticArrays.currentMode == "timed")
				PlayerPrefs.SetInt(StaticArrays.timedModeHighScoreKey,StaticArrays.currentScore);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
