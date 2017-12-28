using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStart : MonoBehaviour {

    public Transform startPos, endPos;
    public float lerpSpeed;
    private GameObject[] ground;

	// Use this for initialization
	void Start () {

		ground = GameObject.FindGameObjectsWithTag("GroundBasic");
		
		foreach(GameObject g in ground)
		{
			if(g.name == "Grid-0,0(Clone)")
			  startPos = g.transform.GetChild(0); //getsposition of spawn point
			else if(g.name == "Grid-3,3(Clone)")
			   endPos = g.transform.GetChild(0);
		}

		this.transform.position = startPos.position;

				
	}
	
	// Update is called once per frame
	void Update () {

     move(startPos);
		
	}

	public void move( Transform pos2)
	{
       transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, lerpSpeed);
	}
}
