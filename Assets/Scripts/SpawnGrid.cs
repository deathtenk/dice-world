using UnityEngine;
using System.Collections;

public class SpawnGrid : MonoBehaviour {
	public GameObject cell;
	public int maxX = 500;
	public int maxY = 500;

	void Start () {
		for (int x = 0; x < maxX; x++) 
		{
			Instantiate(cell, new Vector3(x, 0, 0), Quaternion.identity);
			for (int y = 1; y < maxY; y++)
			{
				Instantiate(cell, new Vector3(x, 0, y), Quaternion.identity);
			}
		}
	
	}

}
