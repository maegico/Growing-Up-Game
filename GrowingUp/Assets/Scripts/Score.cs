using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	public int playerScore;
	public List<int> prevScores;
	public bool introPlayed;
	protected bool ScoreAdded = false;

	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad(gameObject);
		print("global start run");
	}

	void Awake() {
		print("global awake run");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
