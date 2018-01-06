using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyController : MonoBehaviour {
    
	
    private bool _isSelected;

    //debug
	public float xpos, ypos;
	public float moveSpeed;
	public guyProperties Guy = new guyProperties();
	private float spawnFactor;
	public bool isMoving;
    
	private GameObject[] validDestinations;
	private Vector3 vector_dest, vector_finalDest;

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
			this.transform.position = Vector3.MoveTowards(this.transform.position,vector_dest,moveSpeed);
		}
		if(this.transform.position == vector_dest && this.transform.position != vector_finalDest)
		{
		 isMoving = false;
		 setMovePath();
		}
	}

	private void OnMouseDown() {
		
		Guy.isSelected = true;
		selectValidDestination();
	}

	public void selectValidDestination(){

		validDestinations = GameObject.FindGameObjectsWithTag("GroundBasic");

		foreach(GameObject dest in validDestinations)
		{
			if(dest.GetComponent<groundController>().myProps.pieceXPosition < 5)
				dest.GetComponent<Select>().setSelectable(this.gameObject);
		}
	}

	public void setFinalDest(GameObject FDest)
	{
		destination = FDest;
		vector_finalDest = FDest.transform.GetChild(0).position;
		foreach(GameObject d in validDestinations)
		{
			d.GetComponent<Select>().setDormant();
		}
		setMovePath();

	}
	
	public void setMovePath()
	{
		//compare my final destination to current path
		//X Difference
		float xDif = destination.GetComponent<groundController>().myProps.pieceXPosition - Guy.myXPosition;
		float yDif = destination.GetComponent<groundController>().myProps.pieceYPosition - Guy.myYPosition;

        
		//move in x axis first
		if(xDif > 0)
		{
			foreach(GameObject d in validDestinations)
			{
				if(Guy.myXPosition+1 == d.GetComponent<groundController>().myProps.pieceXPosition
				    && Guy.myYPosition == d.GetComponent<groundController>().myProps.pieceYPosition)
					{
						vector_dest = d.transform.GetChild(0).position;
						isMoving = true;
						Guy.myXPosition++;
						return;	
					}	
			}
		}
		else if(xDif < 0)
		{
			foreach(GameObject d in validDestinations)
			{
				if(Guy.myXPosition-1 == d.GetComponent<groundController>().myProps.pieceXPosition
				    && Guy.myYPosition == d.GetComponent<groundController>().myProps.pieceYPosition)
					{
						vector_dest = d.transform.GetChild(0).position;
						isMoving = true;
						Guy.myXPosition--;
						return;	
					}	
			}
		}
		//then move in y if nothing to move in x
		if(yDif > 0)
		{
			foreach(GameObject d in validDestinations)
			{
				if(Guy.myYPosition+1 == d.GetComponent<groundController>().myProps.pieceYPosition
				    && Guy.myXPosition == d.GetComponent<groundController>().myProps.pieceXPosition)
					{
						vector_dest = d.transform.GetChild(0).position;
						isMoving = true;
						Guy.myYPosition++;
						return;	
					}	
			}
		}
		else if(yDif < 0)
		{
			foreach(GameObject d in validDestinations)
			{
				if(Guy.myYPosition-1 == d.GetComponent<groundController>().myProps.pieceYPosition
				    && Guy.myXPosition == d.GetComponent<groundController>().myProps.pieceXPosition)
					{
						vector_dest = d.transform.GetChild(0).position;
						isMoving = true;
						Guy.myYPosition--;
						return;	
					}	
			}
		}

	}

	

}
