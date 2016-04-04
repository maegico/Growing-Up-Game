using UnityEngine;
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
        AudioController = GameObject.Find("Global").GetComponent<AudioControl>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayGame()
    {
        Application.LoadLevel(1);
        AudioController.SetMelodyVol(1f);
        AudioController.SetTeenVol(0f);
        AudioController.SetAdultVol(0f);
    }

	public void MainMenu() {
		Application.LoadLevel(0);
	}

	public void Scores() {
		Application.LoadLevel(2);
	}

    private void LoadLevelByName()
    {
        
    }
}
