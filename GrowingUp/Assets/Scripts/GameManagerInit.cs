using UnityEngine;
using System.Collections;

public class GameManagerInit : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
