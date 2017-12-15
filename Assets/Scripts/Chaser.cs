﻿using UnityEngine;
using System.Collections;



public class Chaser : MonoBehaviour {
	
	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;

	// Use this for initialization
	void Start () 
	{
		
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;

		// face the target
		transform.LookAt(target);

		//distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);

		// the chaser is farther away than the minimum distance move towards it at rate speed.
		if(distance > minDist)	
			transform.position += transform.forward * speed * Time.deltaTime;	
	}


	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

}
