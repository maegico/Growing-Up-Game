﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour {

    protected Dictionary<string, int> Levels;

    protected AudioControl AudioController;

	// Use this for initialization
	void Start () {
        Levels = new Dictionary<string, int>();
        Levels.Add("MainMenu", 0); Levels.Add("Main Menu", 0);
        Levels.Add("Game", 1); Levels.Add("Default", 1);
        //AudioController = GameObject.Find("AudioController").GetComponent<AudioControl>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayGame()
    {
        Application.LoadLevel(1);
        //AudioController.SetMelodyVol(1f);
    }

	public void MainMenu() {
		Application.LoadLevel(0);
	}

    private void LoadLevelByName()
    {
        
    }
}
