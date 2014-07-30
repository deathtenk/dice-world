using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell : Automaton {

	private int state;
	private GameObject auto;

	void Start () {
		auto = GameObject.FindGameObjectWithTag("automaton");
		// transform.parent = auto.transform;
		//a = transform.parent.GetComponent<Automaton>();
		int stateRoll = Random.Range(0, 100);
		if (stateRoll <= 35)
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

	public void StateSwitch(Dictionary <Vector3, Cell> locbp)
	{
		int t = DetermineState(locbp);
		if (t >= 4)
			this.State = 1;
		else
			this.State = 0;
	}

}
