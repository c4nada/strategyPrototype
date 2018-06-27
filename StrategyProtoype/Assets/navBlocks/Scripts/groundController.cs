using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundController : MonoBehaviour {
    

	public groundProperties myProps = new groundProperties();
	
	private float spawnFactor;
	public bool isOccupied;
	public float xpos, ypos;
	// Use this for initialization
	void Start () {

		spawnFactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapGridGenerator>().spawnFactor;
		myProps.pieceXPosition = this.gameObject.transform.position.x / spawnFactor;
		myProps.pieceYPosition = this.gameObject.transform.position.z / spawnFactor;
		myProps.isSelected = false;
		myProps.isOccupied = false;

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void resetMyPosition()
	{
	    myProps.pieceXPosition = this.gameObject.transform.position.x / spawnFactor;
		myProps.pieceYPosition = this.gameObject.transform.position.z / spawnFactor;
	}

	public void setIsOccupiedFlag(bool flag)
	{
		myProps.isOccupied = flag;
	}
}
