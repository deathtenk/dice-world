using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Automaton : MonoBehaviour {

	public Cell[] allCells;
	public Dictionary<Vector3, Cell> listOfCellsByPosition;
	

	void OnMouseDown()
	{
		allCells = FindObjectsOfType(typeof(Cell)) as Cell[];
		listOfCellsByPosition = OrganizeCellsByPosition();
		Organize(listOfCellsByPosition);

	}

	public void Organize(Dictionary <Vector3, Cell> ocbp)
	{

		foreach (Cell cell in allCells)
			cell.StateSwitch(ocbp);
	}

	public Dictionary<Vector3, Cell> OrganizeCellsByPosition()
	{
		Dictionary<Vector3, Cell> x = new Dictionary<Vector3, Cell>();
		foreach (Cell cell in allCells) 
		{
			x.Add(cell.transform.position, cell);
		}
		return x;
	}

	public int DetermineState(Dictionary<Vector3, Cell> locbp)
	{
		
		Vector3 cp = transform.position;
		Vector3 s = new Vector3 (cp.x, cp.y, cp.z - 1.0F);
		Vector3 n = new Vector3 (cp.x, cp.y, cp.z + 1.0F);
		Vector3 e = new Vector3 (cp.x + 1.0F, cp.y, cp.z);
		Vector3 w = new Vector3 (cp.x - 1.0F, cp.y, cp.z);
		Vector3 se = new Vector3 (cp.x + 1, cp.y, cp.z - 1.0F);
		Vector3 sw = new Vector3 (cp.x - 1.0F, cp.y, cp.z - 1.0F);
		Vector3 ne = new Vector3 (cp.x - 1.0F, cp.y, cp.z + 1.0F);
		Vector3 nw = new Vector3 (cp.x + 1.0F, cp.y, cp.z + 1.0F);
		int t = 0;
		
		if (locbp.ContainsKey(s)) 
		{
			Cell c = locbp[s];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(n)) 
		{
			Cell c = locbp[n];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(e)) 
		{
			Cell c = locbp[e];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(w)) 
		{
			Cell c = locbp[w];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(se)) 
		{
			Cell c = locbp[se];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(sw)) 
		{
			Cell c = locbp[sw];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(ne)) 
		{
			Cell c = locbp[ne];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (locbp.ContainsKey(nw)) 
		{
			Cell c = locbp[nw];
			if (c.State == 1)
			{
				t++;
			}
		}
		return t;
		
	}

}
