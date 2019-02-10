using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyAIController : MonoBehaviour {

    private bool _setIsOccupiedOnce, _destroy;

	public float health, damageDone;
	public float moveSpeed,moveRange,attackRange, baseActionPoints;
	public guyProperties Guy = new guyProperties();
	private float spawnFactor;
	public bool isMoving,myTurn;
	public int team;
    
	private GameObject[]  allTiles;
	private List<GameObject> _allGuys = new List<GameObject>();
	private List<GameObject> validDestinations = new List<GameObject>();
	private Vector3 vector_dest, vector_finalDest;
	private GameObject destination, gameController;
	void Start () {

		gameController = GameObject.FindGameObjectWithTag("GameController");

		spawnFactor = gameController.GetComponent<MapGridGenerator>().spawnFactor;

		allTiles = GameObject.FindGameObjectsWithTag("GroundBasic");
		_allGuys.AddRange(GameObject.FindGameObjectsWithTag("Guy"));
		

		this.gameObject.transform.localPosition = new Vector3 (this.transform.position.x,2,this.transform.position.z);
        
		//default values...
		if(health <= 0)
			health = 10;
		if(damageDone <= 0)
			damageDone = 1;

		//set initial grid position based on spawn factor and spawn location
		Guy.isSelected = false;
		Guy.myXPosition =  this.gameObject.transform.localPosition.x / spawnFactor;
		Guy.myYPosition =  this.gameObject.transform.localPosition.z / spawnFactor;
		Guy.moveRange = moveRange;
		Guy.attackRange = attackRange;
		Guy.team = team;
		Guy.selectable = false;
		Guy.health = health;
		Guy.damageDone = damageDone;
		Guy.actionPoints = baseActionPoints;
		Guy.selectable = false;
		

		_setIsOccupiedOnce = true; //because unity isnt setting pos at startup properly...
		_destroy = false;


	}
	void Update () {

		//Do once post start
		if(_setIsOccupiedOnce)
		{
		 setIsOccupied(Guy.myXPosition, Guy.myYPosition, true);
		 _setIsOccupiedOnce = false;
		}
		//move the guy
		if(isMoving)
		{
			this.transform.position = Vector3.MoveTowards(this.transform.position,vector_dest,moveSpeed);
		}
		if(this.transform.position == vector_dest && this.transform.position != vector_finalDest)
		{
		 isMoving = false;
		 setMovePath();
		}
		//after moving do an action if you can
		else if(this.transform.position == vector_finalDest)
		{
			validDestinations.Clear();
			vector_finalDest = new Vector3(0,0,0);
		}

		if(_destroy)
		   Destroy(this.gameObject);
		
	}

	private void OnMouseDown() {

		//this is used for debug only
	}

	

	public void selectValidDestination(){

        float pieceX = 0;
		float pieceY = 0;
		bool isOccupied;
		foreach(GameObject d in allTiles)
		{
			pieceX = d.GetComponent<groundController>().myProps.pieceXPosition;
			pieceY = d.GetComponent<groundController>().myProps.pieceYPosition;
		    isOccupied = d.GetComponent<groundController>().myProps.isOccupied;
			
			//get all valid locations based on move range
			 if (pieceX >= Guy.myXPosition   - (moveRange )  
			        && pieceY >= Guy.myYPosition - (moveRange)
					&& pieceX <= Guy.myXPosition + (moveRange )
					&& pieceY <= Guy.myYPosition + (moveRange )
					&& Mathf.Abs(Guy.myXPosition - pieceX) + Mathf.Abs(Guy.myYPosition - pieceY) <= moveRange
					&& isOccupied == false)
				validDestinations.Add(d);
		}
	}

	public void setFinalDest(GameObject FDest)
	{
		float finalXPos = FDest.GetComponent<groundController>().myProps.pieceXPosition;
		float finalYPos = FDest.GetComponent<groundController>().myProps.pieceYPosition;

		destination = FDest;
		vector_finalDest = FDest.transform.GetChild(0).position;
		setMovePath();

		//set the final position to be occupied
		setIsOccupied(finalXPos,finalYPos,true);
		//minus action points and check if turn should end
		ModifyActionPoints(-2); //THIS IS HARDCODED FOR NOW
		gameController.GetComponent<GameState>().checkActionPoints();

	}

	public void attackUnit(GameObject attackLocation)
	{
		//get the tile position we are attacking
		float attackXLocation = attackLocation.GetComponent<groundController>().myProps.pieceXPosition;
		float attackYLocation = attackLocation.GetComponent<groundController>().myProps.pieceYPosition;

		ModifyActionPoints(-2);
		gameController.GetComponent<GameState>().checkActionPoints();
		
		//Attack the guy with that position
		foreach(GameObject guy in _allGuys)
		{
			float guyX = guy.GetComponent<guyController>().Guy.myXPosition;
			float guyY = guy.GetComponent<guyController>().Guy.myYPosition;
			int team = guy.GetComponent<guyController>().Guy.team;

			if(guyX == attackXLocation && guyY == attackYLocation && this.Guy.team != team)
			{
				guy.GetComponent<guyController>().takeDamage(Guy.damageDone);
				//_allGuys.Remove(guy);
				return;
			}
		}
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
			foreach(GameObject d in allTiles)
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
			foreach(GameObject d in allTiles)
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
			foreach(GameObject d in allTiles)
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
			foreach(GameObject d in allTiles)
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

	public void setIsOccupied(float xpos, float ypos, bool flag)
	{
		float pieceX,pieceY;
		foreach(GameObject d in allTiles)
		{
			pieceX = d.GetComponent<groundController>().myProps.pieceXPosition;
			pieceY = d.GetComponent<groundController>().myProps.pieceYPosition;
			string temp = d.GetComponent<groundController>().name;
			

		 if (pieceX == xpos &&  pieceY == ypos)
			{
	    		d.GetComponent<groundController>().setIsOccupiedFlag(flag);
			}

		}
	}

	public void takeDamage(float damageDone)
	{
		health -= damageDone;
		if(health <= 0)
		{
			foreach(GameObject g in _allGuys)
		    {
				this.gameObject.tag = "Untagged";
				if(this.gameObject != g)
					g.GetComponent<guyController>().reset_allGuys();
			}
			setIsOccupied(Guy.myXPosition, Guy.myYPosition, false);
			_destroy = true;
		}
	}


	public void reset_allGuys()
	{
		_allGuys.Clear();
		_allGuys.AddRange(GameObject.FindGameObjectsWithTag("Guy"));
	}

	public void ModifyActionPoints(float modificationValue)
	{
		Guy.actionPoints += modificationValue;

		if(Guy.actionPoints == 0)
		{
		   Guy.actionPoints = 0;
		   //DO INACTIVE LOGIC HERE
		   Debug.Log(string.Format("{0} turn is over", this.name));
		}

		
	}

	

	

	 
}
