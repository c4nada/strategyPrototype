using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {

    public Material dormant,selectable,selected;
    
    private Material _activeMat;
	
	private bool chosen;
	// Use this for initialization
	void Start () {


		_activeMat = dormant;
		chosen = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.GetComponent<MeshRenderer>().material = _activeMat;
	}

	private void OnMouseEnter() {
        if(!chosen)
			_activeMat = selectable;
		
	}

	private void OnMouseExit() {
		if(!chosen)
			_activeMat = dormant;
	}

	private void OnMouseDown() {
		_activeMat = selected;
		
		if(!chosen)
			chosen = true;
		else
		{
		    chosen = false;
			_activeMat = selectable;
		}
		
		   
	}
}
