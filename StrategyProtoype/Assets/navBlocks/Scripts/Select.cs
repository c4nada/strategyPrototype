using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {

    public Material dormant,mat_selectable,selected,mat_attackable;
        
    private Material _activeMat;
	
    public GameObject _gamecontroller, _guySelected;

	private Component _groundProps;

	public bool chosen,selectable,attackable;
	// Use this for initialization
	void Start () {
        
		_gamecontroller = GameObject.FindGameObjectWithTag("GameController");

		_activeMat = dormant;
		chosen = false;
		_groundProps = this.GetComponent<groundController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.GetComponent<MeshRenderer>().material = _activeMat;
	}

	/*private void OnMouseEnter() {
        if(!chosen)
			_activeMat = selectable;
		
	}*/

	public void setAttackable(GameObject arg)
	{
		_activeMat = mat_attackable;
		attackable = true;
		_guySelected = arg;
	}


	public void setSelectable(GameObject arg) 
	{
		_activeMat = mat_selectable;
		selectable = true;
		_guySelected = arg;
	}

	public void setDormant()
	{
		selectable = false;
		attackable = false;	
		_activeMat = dormant;
	}

	/*private void OnMouseExit() {
		if(!chosen)
			_activeMat = dormant;
	} */

	private void OnMouseDown() {

		if(selectable)
		{ 
			float xPos = _guySelected.GetComponent<guyController>().Guy.myXPosition;
			float yPos = _guySelected.GetComponent<guyController>().Guy.myYPosition;

			_guySelected.GetComponent<guyController>().setFinalDest(this.gameObject);
			//set the starting tile to be unoccupied if i selected one
			_guySelected.GetComponent<guyController>().setIsOccupied(xPos,yPos,false);
			selectable = false;

		}
		//we can only attack a tile if its occupied, if so pass it to the guy to do the logic
		else if(attackable && this.GetComponent<groundController>().myProps.isOccupied)
		{
			_guySelected.GetComponent<guyController>().attackUnit(this.gameObject);
			attackable = false;
		}
		//if no valid action on tile is taken then set dormant...
		else if(_guySelected != null)
		{
			 _guySelected.GetComponent<guyController>().setAllTilesDormant();
			// _guySelected.GetComponent<guyController>().Guy.isSelected = false;
		}
		
	}
}
