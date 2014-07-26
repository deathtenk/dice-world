using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	private int state;

	void Start () {
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

}
