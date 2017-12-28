using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public int ActivePlayer;

	public GameObject player;

	// Use this for initialization
	void Start () {

		ActivePlayer =1;

		Instantiate(player);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
}
