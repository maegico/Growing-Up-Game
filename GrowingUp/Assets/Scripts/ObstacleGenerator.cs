using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

    float timer;
    List<Obstacle> obstacles;

    // Use this for initialization
    void Start () {
        timer = 3;
        obstacles = new List<Obstacle>();
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
