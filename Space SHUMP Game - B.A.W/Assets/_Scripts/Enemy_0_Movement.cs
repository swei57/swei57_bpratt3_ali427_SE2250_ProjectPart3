using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0_Movement : MonoBehaviour {

	public float speed = .1f;
	private Vector3 direction = new Vector3(0f,-1f,0f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (direction * speed);
}
}