using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyController : MonoBehaviour {
    
	
    private bool _isSelected;

    //debug
	public float xpos, ypos;
	public float moveSpeed,moveRange,attackRange;
	public guyProperties Guy = new guyProperties();
	private float spawnFactor;
	public bool isMoving;
    
	private GameObject[]  allTiles;
	private List<GameObject> validDestinations = new List<GameObject>();
	private Vector3 vector_dest, vector_finalDest;

	private GameObject destination;
	// Use this for initialization
	void Start () {

		spawnFactor = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapGridGenerator>().spawnFactor;

		allTiles = GameObject.FindGameObjectsWithTag("GroundBasic");

		this.gameObject.transform.localPosition = new Vector3 (this.transform.position.x,2,this.transform.position.z);

		//set initial grid position based on spawn factor and spawn location
		Guy.isSelected = false;
		Guy.myXPosition =  this.gameObject.transform.localPosition.x / spawnFactor;
		Guy.myYPosition =  this.gameObject.transform.localPosition.z / spawnFactor;
		Guy.moveRange = moveRange;
		Guy.attackRange = attackRange;

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
		else if(this.transform.position == vector_finalDest)
		{
			validDestinations.Clear();
			vector_finalDest = new Vector3(0,0,0);
			checkAttack();
			
		}
		
	}

	private void OnMouseDown() {
		
		Guy.isSelected = true;
		selectValidDestination();
	}

	public void selectValidDestination(){

        float pieceX = 0;
		float pieceY = 0;
		foreach(GameObject d in allTiles)
		{
			pieceX = d.GetComponent<groundController>().myProps.pieceXPosition;
			pieceY = d.GetComponent<groundController>().myProps.pieceYPosition;
			//get all horizontal and vertical valid locations
			//get Horizontal X
			//NOTE: this could be improved by using the abs function for all...maybe
			if(pieceX >= Guy.myXPosition - moveRange 
			   && pieceX <= Guy.myXPosition + moveRange
			   && pieceY == Guy.myYPosition)
				validDestinations.Add(d);
			//Get horizontal Y
			else if(pieceY >= Guy.myYPosition - moveRange
			        && pieceY <= Guy.myYPosition + moveRange
		 	        && pieceX == Guy.myXPosition)
			    validDestinations.Add(d);
			//get valid locations diagnol
			else if (pieceX >= Guy.myXPosition   - (moveRange - 1)  
			        && pieceY >= Guy.myYPosition - (moveRange -1)
					&& pieceX <= Guy.myXPosition + (moveRange -1)
					&& pieceY <= Guy.myYPosition + (moveRange -1)
					&& Mathf.Abs(Guy.myXPosition - pieceX) + Mathf.Abs(Guy.myYPosition - pieceY) <= moveRange)
				validDestinations.Add(d);
		}
		foreach(GameObject d in validDestinations)
		{
			pieceX = d.GetComponent<groundController>().myProps.pieceXPosition;
			pieceY = d.GetComponent<groundController>().myProps.pieceYPosition;
			
			if( Guy.myXPosition != pieceX
			   || Guy.myYPosition != pieceY)
			{
				d.GetComponent<Select>().setSelectable(this.gameObject);
			}
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
	public void checkAttack()
	{
		float pieceX = 0;
		float pieceY = 0;
		
		//check surrounder horizontal tiles
		foreach(GameObject d in allTiles)
		{
			pieceX = d.GetComponent<groundController>().myProps.pieceXPosition;
			pieceY = d.GetComponent<groundController>().myProps.pieceYPosition;
			
			if(pieceX >= Guy.myXPosition - attackRange 
			   && pieceX <= Guy.myXPosition + attackRange
			   && pieceY == Guy.myYPosition)
			{
				d.GetComponent<Select>().setAttackable();
			}
			//Get horizontal Y
			else if(pieceY >= Guy.myYPosition - attackRange
			        && pieceY <= Guy.myYPosition + attackRange
		 	        && pieceX == Guy.myXPosition)
			{
				d.GetComponent<Select>().setAttackable();
			}

			if( Guy.myXPosition == pieceX
			   && Guy.myYPosition == pieceY)
			   d.GetComponent<Select>().setDormant();

		}


	}

	

}
