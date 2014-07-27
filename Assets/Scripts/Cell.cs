using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : Automaton {

	private int state;
	private GameObject auto;
	private Automaton a;
	int t = 0;

	void Start () {
		auto = GameObject.FindGameObjectWithTag("automaton");
		transform.parent = auto.transform;
		a = transform.parent.GetComponent<Automaton>();
		int stateRoll = Random.Range(0, 100);
		if (stateRoll <= 25)
			state = 1;
		else
			state = 0;
			
		switch (state) 
		{
			case 0: StateZero(); break;
			case 1: StateOne (); break;
		}
	}

	void StateZero()	{
		renderer.material.color = Color.blue;
	
	}

	void StateOne()	{
		renderer.material.color = Color.red;
	}

	public void DetermineState()
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

		if (a.listOfCellsByPosition.ContainsKey(s)) 
		{
			Cell c = a.listOfCellsByPosition[s];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(n)) 
		{
			Cell c = a.listOfCellsByPosition[n];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(e)) 
		{
			Cell c = a.listOfCellsByPosition[e];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(w)) 
		{
			Cell c = a.listOfCellsByPosition[w];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(se)) 
		{
			Cell c = a.listOfCellsByPosition[se];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(sw)) 
		{
			Cell c = a.listOfCellsByPosition[sw];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(ne)) 
		{
			Cell c = a.listOfCellsByPosition[ne];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (a.listOfCellsByPosition.ContainsKey(nw)) 
		{
			Cell c = a.listOfCellsByPosition[nw];
			if (c.State == 1)
			{
				t++;
			}
		}
		if (t >= 4)
			this.State = 1;
		else
			this.State = 0;

	}

	public int State 
	{
		get
		{
			return state;
		}
		set
		{
			state = value;
			switch (state) 
			{
				case 0: StateZero(); break;
				case 1: StateOne (); break;
			}
		}
	}

}
