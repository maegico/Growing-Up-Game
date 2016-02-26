﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

    //(1.042, 0.021, 0) - child of roller

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
        if (obstacles.Count < 5 && timer <= 0)
        {
            //not sure why this is giving me errors
            obstacles.Add((Obstacle)Instantiate(obstacles[0], new Vector3(-0.5f, 12.5f, 31.9f), Quaternion.identity));
            //we will see if this script works
            obstacles[obstacles.Count - 1].transform.SetParent(GameObject.FindGameObjectWithTag("roller").transform, false);
            //Generate Obstacle
            timer = 3;
        }
    }
}
