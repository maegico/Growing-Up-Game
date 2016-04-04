using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreUI : MonoBehaviour {

	protected Score score;
	public Score scorePrefab;
	public Text names;
	public Text dates;
	protected string[] defaultNames = {
		"Old Fart",
		"Midlife",
		"Adult",
		"Teen",
		"Tween",
		"Kid",
		"Toddler",
		"Baby",
	};
	protected int[] defaultScores = {
		30009,
		13006,
		10502,
		7001,
		4505,
		2500,
		1000,
		500
	};

	// Use this for initialization
	void Start () {
		// acquire score object
		if (GameObject.FindObjectOfType<Score>() == null) {
			score = GameObject.Instantiate(scorePrefab);
		} else {
			score = GameObject.FindObjectOfType<Score>();
		}

		// set names and dates
		string nameStr = "";
		string dateStr = "";
		if (defaultScores[0] < score.playerScore) {
			nameStr += "YOUR SCORE\n";
			dateStr += ConvertIntToDateString(score.playerScore) + "\n";
		} else {
			nameStr += defaultNames[0]+"\n";
			dateStr += ConvertIntToDateString(defaultScores[0]) + "\n";
		}
		for (int i=1; i<defaultNames.Length; i++) {
			if (defaultScores[i] < score.playerScore && defaultScores[i-1] > score.playerScore) {
				nameStr += "YOUR SCORE\n";
				dateStr += ConvertIntToDateString(score.playerScore) + "\n";
			} else {
				nameStr += defaultNames[i]+"\n";
				dateStr += ConvertIntToDateString(defaultScores[i]) + "\n";
			}
		}

		// set text
		names.text = nameStr;
		dates.text = dateStr;
		print(nameStr);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	protected int startYear = 2000; // set in inspector


	// month names
	public string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
	// month lengths
	public int[] monthDays = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

	string ConvertIntToDateString(int d)
	{
		// decide what year it is
		int year = startYear + (int)Mathf.Floor(d / 360);
		bool leapYear = (year % 4 == 0);
		int daysThisYear = d % 360;
		int tally = daysThisYear;
		string month = "";
		int day = 0;
		for (int i = 0; i < monthDays.Length; i++)
		{
			if (tally < monthDays[i])
			{
				month = months[i];
				day = tally+1;
				break;
			}
			else
			{
				tally -= monthDays[i];

			}
		}
		return (month + " " + day + ", " + year);
	}
}
