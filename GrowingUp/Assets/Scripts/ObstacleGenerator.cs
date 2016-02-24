using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

    float timer;
    List<Obstacle> obstacles;

	public List<Obstacle> Obstacles {
		get {
			return obstacles;
		}

	}

    // Use this for initialization
    void Start () {
        timer = 3;
        obstacles = new List<Obstacle>();
		// for now we'll fill this list with the one obstacle in the game
		obstacles.Add(GameObject.FindObjectOfType<Obstacle>());
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //Generate Obstacle
            timer = 3;
        }
    }
}
