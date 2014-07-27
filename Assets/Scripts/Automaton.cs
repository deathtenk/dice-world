using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Automaton : MonoBehaviour {

	public Cell[] allCells;
	public Dictionary<Vector3, Cell> listOfCellsByPosition;

	void Update () 
	{
	}

	void OnMouseDown()
	{
		allCells = FindObjectsOfType(typeof(Cell)) as Cell[];
		listOfCellsByPosition = ListCellsByPosition();
		gameObject.BroadcastMessage("DetermineState");
	}

	public Dictionary<Vector3, Cell> ListCellsByPosition()
	{
		Dictionary<Vector3, Cell> acbp = new Dictionary<Vector3, Cell>();
		foreach (Cell cell in allCells) 
		{
			acbp.Add(cell.transform.position, cell);
		}
		return acbp;
	}

	void OrderedState()
	{
		foreach (Cell cell in allCells)
		{
			cell.DetermineState();
		}
	}

}
