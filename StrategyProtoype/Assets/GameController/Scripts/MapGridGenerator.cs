using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridGenerator : MonoBehaviour {

	public GameObject groundGrid;
    public int xGridSize, yGridSize,spawnFactor;	
	private int[,] grid;

	// Use this for initialization
	void Start () {

		if(xGridSize == 0)
		   xGridSize = 4; //default
		else if(yGridSize == 0)
		   yGridSize = 4; //default   

		grid = new int[xGridSize,yGridSize];   

       
		//create the grid with 0 values
		for(int i = 0; i < xGridSize; i++)
		{
			for(int j = 0; j < yGridSize; j++)
			{
               grid[i,j] = 0;
			   groundGrid.name = string.Format("Grid-{0},{1}", i, j);
			   Instantiate(groundGrid, setSpawnPoint(i,j), groundGrid.transform.rotation);
		   
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 setSpawnPoint(float xpos, float zPos)
	{
		Vector3 spawn;

		spawn.x = xpos * spawnFactor ;
		spawn.z = zPos * spawnFactor;

		spawn.y = 0; //always at 0

		return spawn;
	}


}
