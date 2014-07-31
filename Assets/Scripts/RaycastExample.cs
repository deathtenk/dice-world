﻿using UnityEngine;
using System.Collections;

public class RaycastExample : MonoBehaviour {

	public GameObject terrain;
	private PolygonGenerator tScript;
	public GameObject target;
	private LayerMask layerMask = (1 << 0);

	void Start () {
		tScript=terrain.GetComponent ("PolygonGenerator") as PolygonGenerator;
	}
	
	void Update () {
		RaycastHit hit;
		float distance=Vector3.Distance(transform.position, target.transform.position);
		if (Physics.Raycast (transform.position, (target.transform.position - transform.position).normalized, out hit, distance, layerMask))
		{
			//Debug.DrawLine (transform.position, target.transform.position, Color.red);
			Vector2 point = new Vector2(hit.point.x, hit.point.y);
			point+=(new Vector2(hit.normal.x,hit.normal.y))*-0.5f;
			tScript.blocks[Mathf.RoundToInt (point.x-.5f), Mathf.RoundToInt(point.y + 0.5f)] = 0;
			tScript.update = true;
		}
		else
			Debug.DrawLine (transform.position, target.transform.position, Color.blue);
	}
}
