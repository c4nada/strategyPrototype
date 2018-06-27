using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyGameLogic : MonoBehaviour {	

	public float health,damageDone;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void takeDamage(float damageTaken)
	{
		health -= damageTaken;
	}

	public void checkIfDead()
	{
		if(health <= 0)
			Destroy(this.gameObject);
		
	}
}
