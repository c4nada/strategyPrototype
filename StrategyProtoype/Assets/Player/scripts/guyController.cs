using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyController : MonoBehaviour {
    
	
    private bool _isSelected;

    //debug
	public float xpos, ypos;
	public guyProperties Guy = new guyProperties();
	private float spawnFactor;
	public bool isMoving;
    
	private GameObject[] validDestinations;
	private Vector3 vector_dest;

	private GameObject destination;
	// Use this for initialization
	void Start () {

		spawnFactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapGridGenerator>().spawnFactor;

		this.gameObject.transform.localPosition = new Vector3 (this.transform.position.x,2,this.transform.position.z);

		//set initial grid position based on spawn factor and spawn location
		Guy.isSelected = false;
		Guy.myXPosition =  this.gameObject.transform.localPosition.x / spawnFactor;
		Guy.myYPosition =  this.gameObject.transform.localPosition.z / spawnFactor;
		

		//debug
		ypos = Guy.myYPosition;
		xpos = Guy.myXPosition;
	}
	
	// Update is called once per frame
	void Update () {
		//debug
		ypos = Guy.myYPosition;
		xpos = Guy.myXPosition;
		
		if(isMoving)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,vector_dest,.1f);
		}
		if(this.transform.position == vector_dest)
		 isMoving = false;
	}

	private void OnMouseDown() {
		
		Guy.isSelected = true;
		selectValidDestination();
	}

	public void selectValidDestination(){

		validDestinations = GameObject.FindGameObjectsWithTag("GroundBasic");

		foreach(GameObject dest in validDestinations)
		{
			if(dest.GetComponent<groundController>().myProps.pieceXPosition < 3)
				dest.GetComponent<Select>().setSelectable(this.gameObject);
		}
	}

	public void setDest(GameObject dest)
	{
		destination = dest;
		vector_dest = dest.transform.GetChild(0).position;
		foreach(GameObject d in validDestinations)
		{
			if(d.GetComponent<groundController>().myProps.pieceXPosition < 3)
				d.GetComponent<Select>().setDormant();
		}
		isMoving = true;

	}

	

}
