using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Automaton : MonoBehaviour {

	Cell[] allCells;
	void Update () 
	{
	}

	void OnMouseDown()
	{
		allCells = FindObjectsOfType(typeof(Cell)) as Cell[];
		CleanMap();
		//Debug.Log (allCells[300].transform.position);
		//Debug.Log (FindAdjascentCells(allCells[300]).Count);
	}

	void CleanMap()
	{
		foreach (Cell cell in allCells) 
		{
			int t = 0;
			List<Cell> acs = FindAdjascentCells(cell);
			foreach ( Cell ac in acs)
			{
				if (ac.State == 1)
				{
					t += 1;
				}
			}
			if (t >= 4)
			{
				cell.State = 1;
			}
			else
			{
				cell.State = 0;
			}
		}
	}

	List<Cell> FindAdjascentCells( Cell currentCell)
	{
		List<Cell> list = new List<Cell>();
		Vector3 cp = currentCell.transform.position;
		foreach (Cell cell in allCells) 
		{
			if (cell.transform.position == new Vector3(cp.x - 1, 0, cp.z - 1))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x - 1, 0, cp.z ))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x, 0, cp.z - 1))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x + 1, 0, cp.z + 1))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x, 0, cp.z + 1))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x + 1, 0, cp.z))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x + 1, 0, cp.z - 1))
			{
				list.Add(cell);
			}
			else if (cell.transform.position == new Vector3(cp.x - 1, 0, cp.z + 1))
			{
				list.Add(cell);
			}

		}
		return list;

	}


}
