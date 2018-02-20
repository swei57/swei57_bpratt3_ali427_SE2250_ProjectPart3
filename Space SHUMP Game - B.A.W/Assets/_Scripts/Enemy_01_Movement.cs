using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01_Movement : MonoBehaviour {

	private float speed;
	private float position;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.transform.position = new Vector3 (0, 8, 0);

}
}