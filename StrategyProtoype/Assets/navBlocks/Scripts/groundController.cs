using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundController : MonoBehaviour {
    

	public groundProperties myProps = new groundProperties();
	private float spawnFactor;
	// Use this for initialization
	void Start () {

		spawnFactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapGridGenerator>().spawnFactor;
		myProps.pieceXPosition = this.gameObject.transform.position.x / spawnFactor;
		myProps.pieceYPosition = this.gameObject.transform.position.y / spawnFactor;
		myProps.isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
