using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {

    public Material dormant,mat_selectable,selected;
        
    private Material _activeMat;
	
    public GameObject _gamecontroller, _guySelected;

	public bool chosen,selectable;
	// Use this for initialization
	void Start () {
        
		_gamecontroller = GameObject.FindGameObjectWithTag("GameController");

		_activeMat = dormant;
		chosen = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.GetComponent<MeshRenderer>().material = _activeMat;
	}

	/*private void OnMouseEnter() {
        if(!chosen)
			_activeMat = selectable;
		
	}*/

	public void setSelectable(GameObject arg) 
	{
		_activeMat = mat_selectable;
		selectable = true;
		_guySelected = arg;
	}

	public void setDormant()
	{
		selectable = false;
		_activeMat = dormant;
	}

	/*private void OnMouseExit() {
		if(!chosen)
			_activeMat = dormant;
	} */

	private void OnMouseDown() {
		if(selectable)
		{ 
			_guySelected.GetComponent<guyController>().setDest(this.gameObject);
			selectable = false;

		}
		
	}
}
