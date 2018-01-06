using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour {

	private GameObject _initialLocation,_desiredLocation,_selectedunit;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setInitialLocation(GameObject arg)
	{
		_initialLocation = arg;
	}

	public void setDesiredLocation(GameObject arg)
	{
		_desiredLocation = arg;
	}
	public void setSelectedUnit(GameObject arg)
	{
		_selectedunit = arg;
	}
}
