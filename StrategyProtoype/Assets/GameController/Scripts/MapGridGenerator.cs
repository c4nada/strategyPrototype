using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridGenerator : MonoBehaviour {

	public GameObject groundGrid,Guy;
    public int xGridSize, yGridSize,spawnFactor,guyCount;	
	private int[,] grid;

	// Use this for initialization
	void Start () {
        
		if(xGridSize == 0)
		   xGridSize = 4; //default
		else if(yGridSize == 0)
		   yGridSize = 4; //default   

		grid = new int[xGridSize,yGridSize];   

        int guycounter = 0;
		//create the grid with 0 values
		for(int i = 0; i < xGridSize; i++)
		{
			for(int j = 0; j < yGridSize; j++)
			{
               grid[i,j] = 0;
			   groundGrid.name = string.Format("Grid-{0},{1}", i, j);
			   Instantiate(groundGrid, setSpawnPoint(i,j), groundGrid.transform.rotation);

			if(guycounter < guyCount)
			   {
				   Instantiate(Guy, setSpawnPoint(i,j), Guy.transform.rotation);
				   guycounter++;
			   }
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
		spawn.y = 0;
		spawn.z = zPos * spawnFactor; 	

		return spawn;
	}


}
