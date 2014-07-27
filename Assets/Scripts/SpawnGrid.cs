using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnGrid : MonoBehaviour {
	public int maxX = 500;
	public int maxZ = 500;
	public GameObject cell;
	public Hashtable ht = new Hashtable();
	bool done = false;
	

	void Update () {
		if (done == false)
		{
			for (int x = 0; x <= maxX; x++) 
			{
				for (int z = 1; z <= maxZ; z++)
				{
					Instantiate(cell, new Vector3(x, transform.position.y, z), cell.transform.rotation);
				}
			}
			done = true;
		}
	}



}
